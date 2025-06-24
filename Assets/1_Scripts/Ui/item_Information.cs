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
            informationText[0].text = "����";
        else if (myItem_Data.rarity > ItemRarity.Rare)
            informationText[0].text = "����";
        else if (myItem_Data.rarity > ItemRarity.Common)
            informationText[0].text = "���";
        else
            informationText[0].text = "�Ϲ�";

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
                    Type = "Ȱ";
                    break;
                case WeaponType.Sword:
                    Type = "��";
                    break;
                case WeaponType.Hacking:
                    Type = "��ŷ ��ġ";
                    break;
            }
            informationText[3].text = myWeapon.statusEffect == StatusEffect.None|| myWeapon.statusEffect == StatusEffect.Invincible ? $"{Type}\n���ݷ� {myWeapon.baseDamage}\n���� �ӵ� {myWeapon.baseAttackDelay}"
                : $"{Type}\n���ݷ� {myWeapon.baseDamage}\n���� �ӵ� {myWeapon.baseAttackDelay}\n�ο� ȿ�� {myWeapon.statusEffect.ToString()}, {(string)myWeapon.statusEffectPercent.ToString("F2")}%";
        }
        else informationText[3].text = "";
            informationImage.sprite = myItem_Data.itemIcon;
    }
    public void TriggerExit()
    {
        Information.SetActive(false);
    }
}
