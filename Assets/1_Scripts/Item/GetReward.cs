using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetReward : MonoBehaviour
{
    public static GetReward Instance { get; private set; }

    public GameObject itemIconPrefab;
    int rewardCount;
    List<GameObject> itemIconList = new List<GameObject>();
    List<ItemData> rewardItems = new List<ItemData>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRewardsForPlayer(2,5);
            IconSet();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            for(int j =0; j<rewardItems.Count;j++)
            Debug.Log(rewardItems[j].itemName);
        }
    }
    // 스테이지 정보를 받아와서 보상을 생성하는 함수
    public void GenerateRewardsForPlayer(int stage, int room)
    {
        // 드랍되는 최소, 최대 아이템 개수
        int maxItem = 7;
        int minItem = 1;

        // 범위 내 + 진행도에 따라 랜덤
        rewardCount = Random.Range(1, Mathf.Clamp(stage * room, minItem, maxItem));
        
        // 무기 개수 -> 패시브/액티브 개수 -> 아이템 개수 순서대로 결정
        // 패시브/액티브 아이템은 한개or드랍되지 않음
        int weaponCount= Random.Range(1, rewardCount);
        int PassActCount = Random.Range(0, 2);
        int itemCount = rewardCount - PassActCount- weaponCount;
        
        Debug.Log($"Stage: {stage} Room: {room}");

        rewardItems.Clear();

        for (int i = 0; i < weaponCount; i++)
        {
            WeaponItemData wp_DataOriginal = RandomItem.RandomWeaponReward(RandomItem.GenerateRarity(stage, room));
            var wp_Data = WeaponItemData.Instantiate(wp_DataOriginal); // 복제본 생성
         
            if (wp_Data == null)
                continue;

            float bonusStr = Random.Range(wp_Data.attackPowerIncrease.x, wp_Data.attackPowerIncrease.y);
            float bonusUniqueStat = Random.Range(wp_Data.uniqueStatIncrease.x, wp_Data.uniqueStatIncrease.y);
            if (wp_Data.rarity>=ItemRarity.Heroic&& wp_Data.statusEffectChance.y!=0)
            {
                wp_Data.statusEffect = (StatusEffect)Random.Range(1, 4);     // 랜덤으로 상태이상 종류 부여(0.None 제외)
                float bonusEffstat = Random.Range(wp_Data.statusEffectChance.x, wp_Data.statusEffectChance.y);
                wp_Data.statusEffectPercent = Mathf.Clamp(bonusEffstat, 0, 100);
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
    void IconSet()
    {
        if(itemIconList.Count>0)
        {
            for(int i=0;i<itemIconList.Count;i++)
            {
                Destroy(itemIconList[i]);
            }
            itemIconList.Clear();
        }

        for (int i = 0; i < rewardCount; i++)
        {//220
            itemIconList.Add(Instantiate(itemIconPrefab, transform));
            itemIconList[i].GetComponentInChildren<Image>().sprite = rewardItems[i].itemIcon;

            Color outline = Color.white;
            var ri = itemIconList[i].GetComponent<RawImage>();
            switch (rewardItems[i].rarity)
            {
                case ItemRarity.Rare:
                    outline = new Color(0.74f,1,0.9f);
                    break;
                case ItemRarity.Heroic:
                    outline = new Color(0.61f, 0.41f, 1);
                    break;
                case ItemRarity.Legendary:
                    outline = new Color(1, 0.117f, 0.854f);
                    break;
            }
            ri.color = outline;
            itemIconList[i].GetComponent<item_Information>().GetData(rewardItems[i], outline);

            // 정렬
            if (i < 4)
                itemIconList[i].transform.localPosition = new Vector2(466 + 222 * i, 450);
            else
                itemIconList[i].transform.localPosition = new Vector2(466 + 222 * (i - 4), 220);
        }
    }
}