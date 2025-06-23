// FileName: ItemDatabase.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ToList() 등 LINQ 기능을 사용하기 위해 필수

public class ItemDatabase : MonoBehaviour
{
    // 어디서든 쉽게 접근할 수 있도록 싱글톤 패턴 사용
    public static ItemDatabase Instance { get; private set; }

    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음

            // Resources/Items 폴더 안의 모든 ItemData를 불러와 딕셔너리에 저장
            ItemData[] allItems = Resources.LoadAll<ItemData>("Items");
            foreach (var item in allItems)
            {
                if (item.itemID == null || item.itemID == "")
                {
                    Debug.LogError($"{item.name} 애셋에 itemID가 지정되지 않았습니다. 확인해주세요.");
                    continue;
                }

                if (!itemDictionary.ContainsKey(item.itemID))
                {
                    itemDictionary.Add(item.itemID, item);
                }
            }
            Debug.Log($"{itemDictionary.Count}개의 아이템을 데이터베이스에 로드했습니다.");
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

    // ▼▼▼▼▼▼▼▼▼▼ 이 함수가 새로 추가되었습니다! ▼▼▼▼▼▼▼▼▼▼
    /// <summary>
    /// 데이터베이스에 있는 모든 아이템의 리스트를 반환합니다.
    /// LINQ 필터링 등 전체 목록이 필요할 때 사용합니다.
    /// </summary>
    /// <returns>모든 ItemData의 리스트</returns>
    public List<ItemData> GetAllItems()
    {
        // 딕셔너리의 모든 값(ItemData)들을 새로운 리스트로 만들어 반환합니다.
        return itemDictionary.Values.ToList();
    }
}