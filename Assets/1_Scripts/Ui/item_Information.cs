using UnityEngine;
using UnityEngine.UI;

public class item_Information : MonoBehaviour
{
    Text Info_rarity, Info_name, Info_description, Info_stat, Info_myItemstat;
    Image informationImage;
    Transform ItemInfo_Panel;
    ItemData myItem_Data;
    MyItems equip;

    Color rarityColor;
    private void Awake()
    {
        ItemInfo_Panel = transform.parent.Find("ItemInfo_Panel");
        Info_rarity= ItemInfo_Panel.Find("Rarity").GetComponent<Text>();
        Info_name= ItemInfo_Panel.transform.Find("Name").GetComponent<Text>();
        Info_description= ItemInfo_Panel.transform.Find("Description").GetComponent<Text>();
        Info_stat= ItemInfo_Panel.transform.Find("Stat").GetComponent<Text>(); 
        Info_myItemstat= ItemInfo_Panel.transform.Find("myItemStat").GetComponent<Text>();
        informationImage = ItemInfo_Panel.GetComponentInChildren<Image>(); 
    }
    public void GetData(ItemData iconItem, Color color)
    {
        rarityColor = color;
        myItem_Data = iconItem;
    }
    public void TriggerEnter()
    {
        ItemInfo_Panel.gameObject.SetActive(true);

        if (myItem_Data.rarity > ItemRarity.Heroic)
            Info_rarity.text = "전설";
        else if (myItem_Data.rarity > ItemRarity.Rare)
            Info_rarity.text = "영웅";
        else if (myItem_Data.rarity > ItemRarity.Common)
            Info_rarity.text = "희귀";
        else
            Info_rarity.text = "일반";

        Info_rarity.color = rarityColor;

        Info_name.text = myItem_Data.itemName;
        Info_description.text = myItem_Data.description;

        if (myItem_Data.itemType == ItemType.Weapon)
        {
            WeaponItemData myWeapon = (WeaponItemData)myItem_Data;
            string Type = "";
            switch (myWeapon.weaponType)
            {
                case WeaponType.Bow:
                    Type = "활";
                    break;
                case WeaponType.Sword:
                    Type = "검";
                    break;
                case WeaponType.Hacking:
                    Type = "해킹 장치";
                    break;
            }
            Info_stat.text = myWeapon.statusEffect == StatusEffect.None|| myWeapon.statusEffect == StatusEffect.Invincible ? $"{Type}\n공격력 {myWeapon.baseDamage}\n공격 속도 {myWeapon.baseAttackDelay}"
                : $"{Type}\n공격력 {myWeapon.baseDamage}\n공격 속도 {myWeapon.baseAttackDelay}\n부여 효과 {myWeapon.statusEffect.ToString()}, {(string)myWeapon.statusEffectPercent.ToString("F2")}%";
        }
        else Info_stat.text = "";
            informationImage.sprite = myItem_Data.itemIcon;
    }
    public void TriggerExit()
    {
        ItemInfo_Panel.gameObject.SetActive(false);
    }

    public void EquipItemButton()
    {
        switch(myItem_Data.itemType)
        {
            case ItemType.Weapon:
                equip.EquipWeapon((WeaponItemData)myItem_Data);
                myItem_Data = equip.myWeapon ?? null;
                break;

            case ItemType.Active:
                equip.EquipActive(myItem_Data);
                myItem_Data = equip.myActive ?? null;
                break;

            case ItemType.Passive:
                equip.EquipPassive(myItem_Data);
                myItem_Data = equip.myPassive ?? null;
                break;
        }
        equip.Equipped();
    }
}
