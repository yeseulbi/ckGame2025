// FileName: ItemDatabase.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq; // ToList() �� LINQ ����� ����ϱ� ���� �ʼ�

public class ItemDatabase : MonoBehaviour
{
    // ��𼭵� ���� ������ �� �ֵ��� �̱��� ���� ���
    public static ItemDatabase Instance { get; private set; }

    public ItemData None;

    private Dictionary<string, ItemData> itemDictionary = new Dictionary<string, ItemData>();
    private Dictionary<string, WeaponItemData> weaponDictionary = new Dictionary<string, WeaponItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı����� ����

            // Resources/Items ���� ���� ���⸦ ������ ItemData�� �ҷ��� ��ųʸ��� ����
            ItemData[] allItems = Resources.LoadAll<ItemData>("Items");
            foreach (var item in allItems)
            {
                if (item.itemID == null || item.itemID == "")
                {
                    Debug.LogError($"{item.name} �ּ¿� itemID�� �������� �ʾҽ��ϴ�. Ȯ�����ּ���.");
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
            Debug.Log($"{itemDictionary.Count}���� �������� �����ͺ��̽��� �ε��߽��ϴ�.");

            WeaponItemData[] allWeapons = Resources.LoadAll<WeaponItemData>("Weapons");
            foreach (var Weapon in allWeapons)
            {
                if (Weapon.itemID == null || Weapon.itemID == "")
                {
                    Debug.LogError($"{Weapon.name} �ּ¿� itemID�� �������� �ʾҽ��ϴ�. Ȯ�����ּ���.");
                    continue;
                }

                if (!weaponDictionary.ContainsKey(Weapon.itemID))
                {
                    weaponDictionary.Add(Weapon.itemID, Weapon);
                }
            }
            Debug.Log($"{weaponDictionary.Count}���� �������� �����ͺ��̽��� �ε��߽��ϴ�.");
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

    public List<ItemData> GetAllItems()
    {
        // ��ųʸ��� ��� ��(ItemData)���� ���ο� ����Ʈ�� ����� ��ȯ�մϴ�.
        return itemDictionary.Values.ToList();
    }

    public List<WeaponItemData> GetAllWeapons()
    {
        return weaponDictionary.Values.ToList();
    }
}