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
    // �������� ������ �޾ƿͼ� ������ �����ϴ� �Լ�
    public void GenerateRewardsForPlayer(int stage, int room)
    {
        // ����Ǵ� �ּ�, �ִ� ������ ����
        int maxItem = 6;
        int minItem = 1;

        // ���� �� + ���൵�� ���� ����
        int rewardCount = Random.Range(1, Mathf.Clamp(stage * room, minItem, maxItem));
        
        // ���� ���� -> �нú�/��Ƽ�� ���� -> ������ ���� ������� ����
        // �нú�/��Ƽ�� �������� ������� ���� ���� ����
        int Count= Random.Range(1, rewardCount);
        int PassActCount = Random.Range(0, rewardCount - Count);
        int itemCount = rewardCount - PassActCount;
        
        Debug.Log($"Stage: {stage} Room: {room}");

        rewardItems.Clear();

        for (int i = 0; i < Count; i++)
        {
            WeaponItemData wp_DataOriginal = RandomItem.RandomWeaponReward(RandomItem.GenerateRarity(stage, room));
            var wp_Data = WeaponItemData.Instantiate(wp_DataOriginal); // ������ ����
         
            if (wp_Data == null)
                continue;

            float bonusStr = Random.Range(wp_Data.attackPowerIncrease.x, wp_Data.attackPowerIncrease.y);
            float bonusUniqueStat = Random.Range(wp_Data.uniqueStatIncrease.x, wp_Data.uniqueStatIncrease.y);
            if (wp_Data.rarity>=ItemRarity.Heroic)
            {
                wp_Data.statusEffect = (StatusEffect)Random.Range(1, 5);     // �������� �����̻� ���� �ο�(0.None ����)
                float bonusEffstat = Random.Range(wp_Data.statusEffectChance.x, wp_Data.statusEffectChance.y);
                wp_Data.statusEffectPercent = Mathf.Clamp(wp_Data.statusEffectPercent, 0, 100);
            }
            wp_Data.baseDamage = Mathf.Round(wp_Data.baseDamage+bonusStr);
            wp_Data.baseAttackDelay = (float)Mathf.Floor((wp_Data.baseAttackDelay-wp_Data.baseAttackDelay * bonusUniqueStat/100)*100)/100;
            rewardItems.Add(wp_Data);

            Debug.Log($"�̸�: {wp_Data.itemName} / ��͵�: {wp_Data.rarity} / ������: {wp_Data.baseDamage} / ����: {wp_Data.baseAttackDelay} / �����̻�: {wp_Data.statusEffect}, {wp_Data.statusEffectPercent}%");
        }
        for(int i =0; i< PassActCount; i++)
        {
            ItemData PassAct_Data = RandomItem.RandomItemReward(RandomItem.GenerateRarity(stage, room), ItemType.Active, ItemType.Passive);

            if (PassAct_Data == null) continue;

            rewardItems.Add(PassAct_Data);
            Debug.Log($"�̸�: {PassAct_Data.itemName} / ��͵�: {PassAct_Data.rarity} / ����: {PassAct_Data.itemType}");
        }
        for (int i = 0; i < itemCount; i++)
        {
            // �Ҹ�ǰ�� ���� ����� �������� �ʱ� ������ ���ٽ����� ��� "����" ���� �Է�
            ItemData item_Data = RandomItem.RandomItemReward(item => item.rarity<= RandomItem.GenerateRarity(stage, room) && item.itemType ==ItemType.Consumable);

            if (item_Data == null) continue;

            rewardItems.Add(item_Data);
            Debug.Log($"�̸�: {item_Data.itemName} / ��͵�: {item_Data.rarity} / ����: {item_Data.itemType}");
        }
    }
}