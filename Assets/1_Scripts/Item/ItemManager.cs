using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RandomItem
{
    // --- 시작 확률 (1-1 방) ---
    private const float COMMON_START = 0.80f;
    private const float HEROIC_START = 0.05f;
    private const float LEGENDARY_START = 0.00f;

    // --- 종료 확률 (3-5 방) ---
    private const float COMMON_END = 0.10f;
    private const float HEROIC_END = 0.40f;
    private const float LEGENDARY_END = 0.30f;

    // --- 전체 진행 단계 수 ---
    private const int TOTAL_STEPS = 14; // (3 스테이지 * 5 방) - 1

    /// <summary>
    /// 스테이지와 방 번호에 따라 4개 등급의 확률을 계산합니다.
    /// </summary>
    /// <param name="stage">현재 스테이지 (1~3)</param>
    /// <param name="room">현재 방 (1~5)</param>
    /// <returns>계산된 확률 (Common, Rare, Heroic, Legendary 순서의 튜플)</returns>
    public static ItemData GenerateReward(int stage, int room)
    {
#region 가중치 계산기

        // 1. 현재 진행도를 0~14 사이의 값으로 계산
        int currentStep = (stage - 1) * 5 + (room - 1);

        // 2. 전체 진행도에서 현재 진행도가 차지하는 비율을 0.0 ~ 1.0으로 계산
        // Mathf.Lerp의 t가 0.0 = a, 1.0  = b 이기 때문이다.
        float progressRatio = (float)currentStep / TOTAL_STEPS;

        // 3. Mathf.Lerp를 사용해 각 등급의 확률을 계산
        float commonChance = Mathf.Lerp(COMMON_START, COMMON_END, progressRatio);
        float heroicChance = Mathf.Lerp(HEROIC_START, HEROIC_END, progressRatio);
        float legendaryChance = Mathf.Lerp(LEGENDARY_START, LEGENDARY_END, progressRatio);

        // 4. 희귀 등급 확률은 전체 합이 1.0이 되도록 나머지를 채웁니다.
        //    (결과적으로 1-1에서는 15%, 3-5에서는 20%가 되도록 자동 계산됩니다)
        float rareChance = 1.0f - commonChance - heroicChance - legendaryChance;

        #endregion
#region 가중치에 의한 하나의 레어도 반환

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
#region 레어도 아이템 리스트에서 1개의 아이템 반환

        // ItemDatabase에서 전체 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 레어도에 맞는 아이템만 추려내기
        .Where(item => item.rarity == chosenRarity).ToList();

        // 하나의 아이템만 고르기(랜덤)
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

    // 무기일 경우의 랜덤 능력치
    public float finalAttackPower;
    public float finalSkillDamage;
    public float finalUniqueStat;
    public float finalStatusChance;

    // 인벤토리에 아이템 보관
    public InventoryItem(ItemData sourceData, int amount = 1)
    {
        data = sourceData;
        quantity = amount;
    }
}