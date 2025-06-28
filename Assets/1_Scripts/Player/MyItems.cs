public class MyItems
{    
    // 내가 장착한 아이템
    public WeaponItemData myWeapon;
    public ItemData myActive;
    public ItemData myPassive;

    public int HealPotion, StrPotion, Defpotion;

    public ItemData ram;

    public void EquipWeapon(WeaponItemData weapon)
    {
        ram = weapon;
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