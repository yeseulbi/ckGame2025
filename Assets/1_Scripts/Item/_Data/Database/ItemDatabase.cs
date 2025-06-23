// FileName: ItemDatabase.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ToList() �� LINQ ����� ����ϱ� ���� �ʼ�

public class ItemDatabase : MonoBehaviour
{
    // ��𼭵� ���� ������ �� �ֵ��� �̱��� ���� ���
    public static ItemDatabase Instance { get; private set; }

    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı����� ����

            // Resources/Items ���� ���� ��� ItemData�� �ҷ��� ��ųʸ��� ����
            ItemData[] allItems = Resources.LoadAll<ItemData>("Items");
            foreach (var item in allItems)
            {
                if (item.itemID == null || item.itemID == "")
                {
                    Debug.LogError($"{item.name} �ּ¿� itemID�� �������� �ʾҽ��ϴ�. Ȯ�����ּ���.");
                    continue;
                }

                if (!itemDictionary.ContainsKey(item.itemID))
                {
                    itemDictionary.Add(item.itemID, item);
                }
            }
            Debug.Log($"{itemDictionary.Count}���� �������� �����ͺ��̽��� �ε��߽��ϴ�.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ ID�� Ư�� ������ �ϳ��� ã�� ��ȯ�ϴ� �Լ�
    public ItemData GetItem(string itemID)
    {
        if (itemDictionary.ContainsKey(itemID))
        {
            return itemDictionary[itemID];
        }
        Debug.LogWarning($"'{itemID}' ID�� ���� �������� �����ͺ��̽����� ã�� �� �����ϴ�.");
        return null;
    }

    // ����������� �� �Լ��� ���� �߰��Ǿ����ϴ�! �����������
    /// <summary>
    /// �����ͺ��̽��� �ִ� ��� �������� ����Ʈ�� ��ȯ�մϴ�.
    /// LINQ ���͸� �� ��ü ����� �ʿ��� �� ����մϴ�.
    /// </summary>
    /// <returns>��� ItemData�� ����Ʈ</returns>
    public List<ItemData> GetAllItems()
    {
        // ��ųʸ��� ��� ��(ItemData)���� ���ο� ����Ʈ�� ����� ��ȯ�մϴ�.
        return itemDictionary.Values.ToList();
    }
}