using UnityEngine;

public class MyItemSet : MonoBehaviour
{
    public static MyItemSet Instance { get; private set; }
    // 내가 장착한 아이템
    public WeaponItemData myWeapon;
    public ItemData myActive;
    public ItemData myPassive;

    public int HealPotion, StrPotion, Defpotion;

    ItemData ram;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipWeapon(WeaponItemData weapon)
    {
        ram = weapon;
        WeaponSet.Instance.WeaponSpriteChange(weapon.itemIcon);
    }

    public void EquipActive(ItemData active)
    {
        if (active.itemType == ItemType.Active)
            ram = active;
    }
    public void EquipPassive(ItemData passive)
    {
        if (passive.itemType == ItemType.Passive)
            ram = passive;
    }
    public void Equipped()
    {
        if (ram.itemType == ItemType.Active)
            myActive = ram;
        else if (ram.itemType == ItemType.Passive)
            myPassive = ram;
        else
            myWeapon = (WeaponItemData)ram;
    }
    public void GetConsumable(ItemData Consumable_type)
    {
        switch (Consumable_type.itemName)
        {
            case "수리 킷 (소)":
                HealPotion++;
                break;
            case "휴대용 역장 생성기":
                Defpotion++;
                break;
            case "임시 공격력 증폭기":
                StrPotion++;
                break;
        }

    }
}
