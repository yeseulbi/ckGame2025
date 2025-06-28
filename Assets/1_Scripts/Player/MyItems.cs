public class MyItems
{    
    // ���� ������ ������
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
            case "���� Ŷ (��)":
                HealPotion++;
                break;
            case "�޴�� ���� ������":
                Defpotion++;
                break;
            case "�ӽ� ���ݷ� ������":
                StrPotion++;
                break;
        }

    }
}