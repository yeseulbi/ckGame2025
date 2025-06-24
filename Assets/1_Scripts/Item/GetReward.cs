using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    List<ItemData> rewardItems = new List<ItemData>();

    private void Start()
    {
    }
    public void ItemNameButton()
    {
        foreach (ItemData item in rewardItems)
        {
            Debug.Log(item);
        }
    }
    public void Button()
    {
        GenerateRewardsForPlayer(2, 5);
    }
    // 스테이지 정보를 받아와서 보상을 생성하는 함수
    public void GenerateRewardsForPlayer(int stage, int room)
    {
        // 드랍되는 최소, 최대 아이템 개수
        int maxItem = 6;
        int minItem = 1;

        // 범위 내 + 진행도에 따라 랜덤
        int rewardCount = Random.Range(1, Mathf.Clamp(stage * room, minItem, maxItem));
        
        // 무기 개수 -> 패시브/액티브 개수 -> 아이템 개수 순서대로 결정
        // 패시브/액티브 아이템은 드랍되지 않을 수도 있음
        int Count= Random.Range(1, rewardCount);
        int PassActCount = Random.Range(0, rewardCount - Count);
        int itemCount = rewardCount - PassActCount;
        
        Debug.Log($"Stage: {stage} Room: {room}");

        rewardItems.Clear();

        for (int i = 0; i < Count; i++)
        {
            WeaponItemData wp_DataOriginal = RandomItem.RandomWeaponReward(RandomItem.GenerateRarity(stage, room));
            var wp_Data = WeaponItemData.Instantiate(wp_DataOriginal); // 복제본 생성
         
            if (wp_Data == null)
                continue;

            float bonusStr = Random.Range(wp_Data.attackPowerIncrease.x, wp_Data.attackPowerIncrease.y);
            float bonusUniqueStat = Random.Range(wp_Data.uniqueStatIncrease.x, wp_Data.uniqueStatIncrease.y);
            if (wp_Data.rarity>=ItemRarity.Heroic)
            {
                wp_Data.statusEffect = (StatusEffect)Random.Range(1, 5);     // 랜덤으로 상태이상 종류 부여(0.None 제외)
                float bonusEffstat = Random.Range(wp_Data.statusEffectChance.x, wp_Data.statusEffectChance.y);
                wp_Data.statusEffectPercent = Mathf.Clamp(wp_Data.statusEffectPercent, 0, 100);
            }
            wp_Data.baseDamage = Mathf.Round(wp_Data.baseDamage+bonusStr);
            wp_Data.baseAttackDelay = (float)Mathf.Floor((wp_Data.baseAttackDelay-wp_Data.baseAttackDelay * bonusUniqueStat/100)*100)/100;
            rewardItems.Add(wp_Data);

            Debug.Log($"이름: {wp_Data.itemName} / 희귀도: {wp_Data.rarity} / 데미지: {wp_Data.baseDamage} / 공속: {wp_Data.baseAttackDelay} / 상태이상: {wp_Data.statusEffect}, {wp_Data.statusEffectPercent}%");
        }
        for(int i =0; i< PassActCount; i++)
        {
            ItemData PassAct_Data = RandomItem.RandomItemReward(RandomItem.GenerateRarity(stage, room), ItemType.Active, ItemType.Passive);

            if (PassAct_Data == null) continue;

            rewardItems.Add(PassAct_Data);
            Debug.Log($"이름: {PassAct_Data.itemName} / 희귀도: {PassAct_Data.rarity} / 종류: {PassAct_Data.itemType}");
        }
        for (int i = 0; i < itemCount; i++)
        {
            // 소모품은 전설 등급이 존재하지 않기 때문에 람다식으로 레어도 "이하" 조건 입력
            ItemData item_Data = RandomItem.RandomItemReward(item => item.rarity<= RandomItem.GenerateRarity(stage, room) && item.itemType ==ItemType.Consumable);

            if (item_Data == null) continue;

            rewardItems.Add(item_Data);
            Debug.Log($"이름: {item_Data.itemName} / 희귀도: {item_Data.rarity} / 종류: {item_Data.itemType}");
        }
    }
}