using UnityEngine;
using UnityEngine.UI;
public class RarityColor
{
    public static readonly Color Common = Color.white;
    public static readonly Color Rare = new Color(0.74f, 1, 0.9f);
    public static readonly Color Heroic = new Color(0.61f, 0.41f, 1);
    public static readonly Color Legendary = new Color(1, 0.117f, 0.854f);
}
public class item_Information : MonoBehaviour
{
    Text Info_rarity, Info_name, Info_description, Info_stat, Info_myItemstat;
    Image informationImage;
    Transform ItemInfo_Panel;
    ItemData myItem_Data;
    RawImage backgroundImage;
    Image iconImage;

    ItemData none;

    Color rarityColor;
    private void Awake()
    {
        backgroundImage = this.GetComponent<RawImage>();
        iconImage = this.GetComponentInChildren<Image>();

        ItemInfo_Panel = transform.parent.GetChild(2);
        Info_rarity= ItemInfo_Panel.Find("Rarity").GetComponent<Text>();
        Info_name= ItemInfo_Panel.transform.Find("Name").GetComponent<Text>();
        Info_description= ItemInfo_Panel.transform.Find("Description").GetComponent<Text>();
        Info_stat= ItemInfo_Panel.transform.Find("Stat").GetComponent<Text>(); 
        //Info_myItemstat= ItemInfo_Panel.transform.Find("myItemStat").GetComponent<Text>();
        informationImage = ItemInfo_Panel.transform.Find("image").GetComponent<Image>(); 
    }
    private void Start()
    {
        none = ItemDatabase.Instance.None;
        IconReset();
    }
    public void IconReset()
    {
        iconImage.sprite = myItem_Data.itemIcon;
        switch (myItem_Data.rarity)
        {
            case ItemRarity.Common:
                rarityColor = RarityColor.Common;
                break;
            case ItemRarity.Rare:
                rarityColor = RarityColor.Rare;
                break;
            case ItemRarity.Heroic:
                rarityColor = RarityColor.Heroic;
                break;
            case ItemRarity.Legendary:
                rarityColor = RarityColor.Legendary;
                break;
        }
        backgroundImage.color = rarityColor;
    }
    public void GetData(ItemData iconItem)
    {
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

        informationImage.sprite = myItem_Data.itemIcon;
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
                MyItemSet.Instance.EquipWeapon((WeaponItemData)myItem_Data);
                myItem_Data = MyItemSet.Instance.myWeapon ?? none;
                break;

            case ItemType.Active:
                MyItemSet.Instance.EquipActive(myItem_Data);
                myItem_Data = MyItemSet.Instance.myActive ?? none;
                break;

            case ItemType.Passive:
                MyItemSet.Instance.EquipPassive(myItem_Data);
                myItem_Data = MyItemSet.Instance.myPassive ?? none;
                break;
            case ItemType.Consumable:
                MyItemSet.Instance.GetConsumable(myItem_Data);
                break;
        }
        MyItemSet.Instance.Equipped();
        StatusUi_Play.Instance.ItemUi_Update();
        IconReset();
        TriggerEnter();
        Debug.Log("버튼 클릭");
    }
}
