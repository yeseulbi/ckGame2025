using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class RandomItem
{
    // --- ���� Ȯ�� (1-1 ��) ---
    private const float COMMON_START = 0.80f;
    private const float HEROIC_START = 0.05f;
    private const float LEGENDARY_START = 0.00f;

    // --- ���� Ȯ�� (3-5 ��) ---
    private const float COMMON_END = 0.40f;
    private const float HEROIC_END = 0.15f;
    private const float LEGENDARY_END = 0.08f;

    // --- ��ü ���� �ܰ� �� ---
    private const int TOTAL_STEPS = 14; // (3 �������� * 5 ��) - 1

    /// <summary>
    /// ���������� �� ��ȣ�� ���� 4�� ����� Ȯ���� ����մϴ�.
    /// </summary>
    /// <param name="stage">���� �������� (1~3)</param>
    /// <param name="room">���� �� (1~5)</param>
    /// <returns>���� Ȯ�� (Common, Rare, Heroic, Legendary ������ Ʃ��)</returns>
    public static ItemRarity GenerateRarity(int stage, int room)
    {
        #region ����ġ ����

        if (room > 5)
            room = 5;
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
        float rareChance = 1.0f - commonChance - heroicChance - legendaryChance;

        #endregion
#region ����ġ�� ���� �ϳ��� ��� ��ȯ

        ItemRarity chosenRarity;
        float randomValue = UnityEngine.Random.value; // 0.0 ~ 1.0

        if (randomValue < legendaryChance)
            chosenRarity = ItemRarity.Legendary;
        else if (randomValue < legendaryChance + heroicChance)
            chosenRarity = ItemRarity.Heroic;
        else if (randomValue < legendaryChance + heroicChance + rareChance)
            chosenRarity = ItemRarity.Rare;
        else
            chosenRarity = ItemRarity.Common;

        return chosenRarity;
        #endregion
    }
    public static WeaponItemData RandomWeaponReward(ItemRarity rarity)
    {
#region ������� 1���� ���� ��ȯ

        // ItemDatabase���� Weapon ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<WeaponItemData> SelectWeaponList = ItemDatabase.Instance.GetAllWeapons()
        //Linq - where�� ����� �´� ���⸸ �߷�����
        .Where(item => item.rarity == rarity).ToList();

        // �ϳ��� ���⸸ ����(����)
        int select = UnityEngine.Random.Range(0, SelectWeaponList.Count);

        return SelectWeaponList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity)
    {
#region ������� 1���� ������(���� ����) ��ȯ

        // ItemDatabase���� Item ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� ����� �´� �����۸� �߷�����
        .Where(item => item.rarity == rarity).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity, ItemType type)
    {
#region ������� Ư�� 1���� ������ ��ȯ

        // ItemDatabase���� Item ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� ���, Ÿ�Կ� �´� �����۸� �߷�����
        .Where(item => item.rarity == rarity && item.itemType == type).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity, ItemType typeA, ItemType typeB)
    {
#region ������� Ư�� 2���� ������ ��ȯ

        // ItemDatabase���� Item ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� ���, Ÿ�Կ� �´� �����۸� �߷�����
        .Where(item => item.rarity == rarity&& item.itemType == typeA||item.itemType==typeB).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemType type)
    {
#region Ư�� 1���� ������ ��ȯ

        // ItemDatabase���� Item ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� Ÿ�Կ� �´� �����۸� �߷�����
        .Where(item => item.itemType == type).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
public static ItemData RandomItemReward(Func<ItemData, bool> condition)
    {
        #region ���ٽ��� �̿��� ���ϴ� ������ ������ ��ȯ

        // ItemDatabase���� Item ������ ����� �޾� SelectItemList�� �Ҵ��ϱ�
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where�� ���ٽ� ���� �Է��ϱ�
        .Where(condition).ToList();

        // �ϳ��� �����۸� ����(����)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
}