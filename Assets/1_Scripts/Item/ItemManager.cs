using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class RandomItem
{
    // --- 시작 확률 (1-1 방) ---
    private const float COMMON_START = 0.80f;
    private const float HEROIC_START = 0.05f;
    private const float LEGENDARY_START = 0.00f;

    // --- 종료 확률 (3-5 방) ---
    private const float COMMON_END = 0.40f;
    private const float HEROIC_END = 0.15f;
    private const float LEGENDARY_END = 0.08f;

    // --- 전체 진행 단계 수 ---
    private const int TOTAL_STEPS = 14; // (3 스테이지 * 5 방) - 1

    /// <summary>
    /// 스테이지와 방 번호에 따라 4개 등급의 확률을 계산합니다.
    /// </summary>
    /// <param name="stage">현재 스테이지 (1~3)</param>
    /// <param name="room">현재 방 (1~5)</param>
    /// <returns>계산된 확률 (Common, Rare, Heroic, Legendary 순서의 튜플)</returns>
    public static ItemRarity GenerateRarity(int stage, int room)
    {
        #region 가중치 계산기

        if (room > 5)
            room = 5;
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
        float rareChance = 1.0f - commonChance - heroicChance - legendaryChance;

        #endregion
#region 가중치에 의한 하나의 레어도 반환

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
#region 레어도에서 1개의 무기 반환

        // ItemDatabase에서 Weapon 아이템 목록을 받아 SelectItemList에 할당하기
        List<WeaponItemData> SelectWeaponList = ItemDatabase.Instance.GetAllWeapons()
        //Linq - where로 레어도에 맞는 무기만 추려내기
        .Where(item => item.rarity == rarity).ToList();

        // 하나의 무기만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectWeaponList.Count);

        return SelectWeaponList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity)
    {
#region 레어도에서 1개의 아이템(무기 제외) 반환

        // ItemDatabase에서 Item 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 레어도에 맞는 아이템만 추려내기
        .Where(item => item.rarity == rarity).ToList();

        // 하나의 아이템만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity, ItemType type)
    {
#region 레어도에서 특정 1종류 아이템 반환

        // ItemDatabase에서 Item 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 레어도, 타입에 맞는 아이템만 추려내기
        .Where(item => item.rarity == rarity && item.itemType == type).ToList();

        // 하나의 아이템만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemRarity rarity, ItemType typeA, ItemType typeB)
    {
#region 레어도에서 특정 2종류 아이템 반환

        // ItemDatabase에서 Item 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 레어도, 타입에 맞는 아이템만 추려내기
        .Where(item => item.rarity == rarity&& item.itemType == typeA||item.itemType==typeB).ToList();

        // 하나의 아이템만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
    public static ItemData RandomItemReward(ItemType type)
    {
#region 특정 1종류 아이템 반환

        // ItemDatabase에서 Item 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 타입에 맞는 아이템만 추려내기
        .Where(item => item.itemType == type).ToList();

        // 하나의 아이템만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
public static ItemData RandomItemReward(Func<ItemData, bool> condition)
    {
        #region 람다식을 이용해 원하는 조건의 아이템 반환

        // ItemDatabase에서 Item 아이템 목록을 받아 SelectItemList에 할당하기
        List<ItemData> SelectItemList = ItemDatabase.Instance.GetAllItems()
        //Linq - where로 람다식 조건 입력하기
        .Where(condition).ToList();

        // 하나의 아이템만 고르기(랜덤)
        int select = UnityEngine.Random.Range(0, SelectItemList.Count);

        return SelectItemList[select];
    }
    #endregion
}