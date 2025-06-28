using UnityEngine;
using UnityEngine.UI;

public class StatusUi_Play : MonoBehaviour
{
    public static StatusUi_Play Instance { get; private set; }
    public GameObject WeaponObj, ActiveObj, PassiveObj;
    Image WeaponIcon, ActiveIcon, PassiveIcon;
    public GameObject HpIcon, EPIcon, UltimateIcon;

    void Awake()
    {
        Instance = this;
        WeaponIcon = WeaponObj.GetComponentInChildren<Image>();
        ActiveIcon = ActiveObj.GetComponentInChildren<Image>();
        PassiveIcon = PassiveObj.GetComponentInChildren<Image>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ItemUi_Update()
    {
        Debug.Log("uiÀû¿ë");
        if(MyItemSet.Instance.myWeapon!=null)
            WeaponIcon.sprite = MyItemSet.Instance.myWeapon.itemIcon;
        if (MyItemSet.Instance.myActive != null)
            ActiveIcon.sprite = MyItemSet.Instance.myActive.itemIcon;
        if (MyItemSet.Instance.myPassive != null)
            PassiveIcon.sprite = MyItemSet.Instance.myPassive.itemIcon;
    }
}
