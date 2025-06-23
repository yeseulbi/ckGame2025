using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RandomItem
{
    // --- ���� Ȯ�� (1-1 ��) ---
    private const float COMMON_START = 0.80f;
    private const float HEROIC_START = 0.05f;
    private const float LEGENDARY_START = 0.00f;

    // --- ���� Ȯ�� (3-5 ��) ---
    private const float COMMON_END = 0.10f;
    private const float HEROIC_END = 0.40f;
    private const float LEGENDARY_END = 0.30f;

    // --- ��ü ���� �ܰ� �� ---
    private const int TOTAL_STEPS = 14; // (3 �������� * 5 ��) - 1

    /// <summary>
    /// ���������� �� ��ȣ�� ���� 4�� ����� Ȯ���� ����մϴ�.
    /// </summary>
    /// <param name="stage">���� �������� (1~3)</param>
    /// <param name="room">���� �� (1~5)</param>
    /// <returns>���� Ȯ�� (Common, Rare, Heroic, Legendary ������ Ʃ��)</returns>
    public static ItemData GenerateReward(int stage, int room)
    {
#region ����ġ ����

        // 1. ���� ���൵�� 0~14 ������ ������ ���
        int currentStep = (stage - 1) * 5 + (room - 1);

        // 2. ��ü ���൵���� ���� ���൵�� �����ϴ� ������ 0.0 ~ 1.0���� ���
        // Mathf.Lerp�� t�� 0.0 = a, 1.0  = b �̱� �����̴�.
        float progressRatio = (float)currentStep / TOTAL_STEPS;

        // 3. Mathf.Lerp�� ����� �� ����� Ȯ���� ���
        float commonChance = Mathf.Lerp(COMMON_START, COMMON_END, progressRatio);
        float heroicChance = Mathf.Lerp(HEROIC_START, HEROIC_END, progressRatio);
        float legendaryChance = Mathf.Lerp(LEGENDARY_START, LEGENDARY_END, progressRatio);

        // 4. ��� ��� Ȯ���� ��ü ���� 1.0�� �ǵ��� �������� ä��ϴ�.
        //    (��������� 1-1������ 15%, 3-5������ 20%�� �ǵ��� �ڵ� ���˴ϴ�)
        float rareChance = 1.0f - commonChance - heroicChance - legendaryChance;

        #endregion
#region ����ġ�� ���� �ϳ��� ��� ��ȯ

        ItemRarity chosenRarity;
        float randomValue = Random.value; // 0.0 ~ 1.0

        if (randomValue < legendaryChance)
            chosenRarity = ItemRarity.Legendary;
        else if (randomValue < legendaryChance + heroicChance)
            chosenRarity = ItemRarity.Heroic;
        else if (randomValue < legendaryChance + heroicChance + rareChance)
            chosenRarity = ItemRarity.Rare;
        else
            chosenRarity = ItemRarity.Common;
        #endregion
#region ��� ������ ����Ʈ���� 1���� ������ ��ȯ

        // ItemDatabase���� ��ü ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� ����� �´� �����۸� �߷�����
        .Where(item => item.rarity == chosenRarity).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
}
[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    // ������ ����� ���� �ɷ�ġ
    public float finalAttackPower;
    public float finalSkillDamage;
    public float finalUniqueStat;
    public float finalStatusChance;

    // �κ��丮�� ������ ����
    public InventoryItem(ItemData sourceData, int amount = 1)
    {
        data = sourceData;
        quantity = amount;
    }
}