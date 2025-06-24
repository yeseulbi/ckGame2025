using UnityEngine;
using UnityEngine.UI;

public class item_Information : MonoBehaviour
{
    Text[] informationText = new Text[4];
    Image informationImage;
    GameObject Information;
    ItemData myItem_Data;
    Color rarityColor;
    private void Awake()
    {
        Information = transform.parent.GetChild(2).gameObject;
        for (int i=0; i<informationText.Length;i++)
        {
            informationText[i] = Information.transform.GetChild(i).GetComponent<Text>();
        }
        informationImage = Information.GetComponentInChildren<Image>(); 
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GetData(ItemData iconItem, Color color)
    {
        rarityColor = color;
        myItem_Data = iconItem;
    }
    public void TriggerEnter()
    {
        Information.SetActive(true);

        if (myItem_Data.rarity > ItemRarity.Heroic)
            informationText[0].text = "전설";
        else if (myItem_Data.rarity > ItemRarity.Rare)
            informationText[0].text = "영웅";
        else if (myItem_Data.rarity > ItemRarity.Common)
            informationText[0].text = "희귀";
        else
            informationText[0].text = "일반";

        informationText[0].color = rarityColor;

        informationText[1].text = myItem_Data.itemName;
        informationText[2].text = myItem_Data.description;

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
            informationText[3].text = myWeapon.statusEffect == StatusEffect.None|| myWeapon.statusEffect == StatusEffect.Invincible ? $"{Type}\n공격력 {myWeapon.baseDamage}\n공격 속도 {myWeapon.baseAttackDelay}"
                : $"{Type}\n공격력 {myWeapon.baseDamage}\n공격 속도 {myWeapon.baseAttackDelay}\n부여 효과 {myWeapon.statusEffect.ToString()}, {(string)myWeapon.statusEffectPercent.ToString("F2")}%";
        }
        else informationText[3].text = "";
            informationImage.sprite = myItem_Data.itemIcon;
    }
    public void TriggerExit()
    {
        Information.SetActive(false);
    }
}
