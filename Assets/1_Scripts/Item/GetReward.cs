using System.Collections.Generic;
using UnityEngine;

public class GetReward : MonoBehaviour
{
    List<ItemData> rewardItems = new List<ItemData>();

    private void Start()
    {
        GenerateRewardsForPlayer(2, 5);
    }

    // �������� ������ �޾ƿͼ� ������ �����ϴ� �Լ�
    public void GenerateRewardsForPlayer(int stage, int room)
    {
        // ���������� ���� ������ ����
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
                        newItemData.statusEffect = (StatusEffect)Random.Range(0, 5);     // enum�� ũ�⸦ �̿��� �������� �����̻� ���� �ο�
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
            Debug.Log($"�̸�: {rewardItems[i].itemName} / ����: {rewardItems[i].description} / ����: {rewardItems[i].itemType} / ��͵�: {rewardItems[i].rarity} / �����̻�: {rewardItems[i].statusEffect}");
        }
    }
}