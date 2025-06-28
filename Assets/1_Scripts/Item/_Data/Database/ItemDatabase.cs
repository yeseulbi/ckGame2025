// FileName: ItemDatabase.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ToList() 등 LINQ 기능을 사용하기 위해 필수

public class ItemDatabase : MonoBehaviour
{
    // 어디서든 쉽게 접근할 수 있도록 싱글톤 패턴 사용
    public static ItemDatabase Instance { get; private set; }

    public ItemData None;

    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();
    private Dictionary<string, WeaponItemData> weaponDictionary = new Dictionary<string, WeaponItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음

            // Resources/Items 폴더 안의 무기를 제외한 ItemData를 불러와 딕셔너리에 저장
            ItemData[] allItems = Resources.LoadAll<ItemData>("Items");
            foreach (var item in allItems)
            {
                if (item.itemID == null || item.itemID == "")
                {
                    Debug.LogError($"{item.name} 애셋에 itemID가 지정되지 않았습니다. 확인해주세요.");
                    continue;
                }
                if (item.itemType == ItemType.Weapon)
                    continue;
                if(item.itemID == "None")
                {
                    None = item;
                }
                else if (!itemDictionary.ContainsKey(item.itemID))
                {
                    itemDictionary.Add(item.itemID, item);
                }
            }
            Debug.Log($"{itemDictionary.Count}개의 아이템을 데이터베이스에 로드했습니다.");

            WeaponItemData[] allWeapons = Resources.LoadAll<WeaponItemData>("Weapons");
            foreach (var Weapon in allWeapons)
            {
                if (Weapon.itemID == null || Weapon.itemID == "")
                {
                    Debug.LogError($"{Weapon.name} 애셋에 itemID가 지정되지 않았습니다. 확인해주세요.");
                    continue;
                }

                if (!weaponDictionary.ContainsKey(Weapon.itemID))
                {
                    weaponDictionary.Add(Weapon.itemID, Weapon);
                }
            }
            Debug.Log($"{weaponDictionary.Count}개의 아이템을 데이터베이스에 로드했습니다.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 아이템 ID로 특정 데이터 하나를 찾아 반환하는 함수
    public ItemData GetItem(string itemID)
    {
        if (itemDictionary.ContainsKey(itemID))
        {
            return itemDictionary[itemID];
        }
        Debug.LogWarning($"'{itemID}' ID를 가진 아이템을 데이터베이스에서 찾을 수 없습니다.");
        return null;
    }

    public List<ItemData> GetAllItems()
    {
        // 딕셔너리의 모든 값(ItemData)들을 새로운 리스트로 만들어 반환합니다.
        return itemDictionary.Values.ToList();
    }

    public List<WeaponItemData> GetAllWeapons()
    {
        return weaponDictionary.Values.ToList();
    }
}