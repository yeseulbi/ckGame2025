using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    List<ItemData> rewardItems = new List<ItemData>();

    private void Start()
    {
        GenerateRewardsForPlayer(2, 5);
    }

    // 스테이지 정보를 받아와서 보상을 생성하는 함수
    public void GenerateRewardsForPlayer(int stage, int room)
    {
        // 스테이지에 따라 보상을 생성
        int rewardCount = Random.Range(1, Mathf.Clamp(stage * room,2,6));

        for (int i = 0; i < rewardCount; i++)
        {
            ItemData newItemData = RandomItem.GenerateReward(stage, room);

            if (newItemData == null)
                continue;
            switch(newItemData.itemType)
            {
                case ItemType.Weapon:
                    if(newItemData.rarity>=ItemRarity.Heroic)
                        newItemData.statusEffect = (StatusEffect)Random.Range(0, 5);     // enum의 크기를 이용해 랜덤으로 상태이상 종류 부여
                    break;

                case ItemType.Passive:

                    break;

                case ItemType.Active:

                    break;

                case ItemType.Consumable:

                    break;

                case ItemType.Key:

                    break;
            }
            rewardItems.Add(newItemData);
            Debug.Log($"이름: {rewardItems[i].itemName} / 설명: {rewardItems[i].description} / 종류: {rewardItems[i].itemType} / 희귀도: {rewardItems[i].rarity} / 상태이상: {rewardItems[i].statusEffect}");
        }
    }
}