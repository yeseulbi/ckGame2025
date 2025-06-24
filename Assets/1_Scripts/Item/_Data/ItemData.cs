// FileName: ItemData.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ������ ����� �����ϴ� �������Դϴ�.
/// </summary>
public enum ItemRarity
{
    Common,     // �Ϲ�
    Rare,       // ���
    Heroic,     // ����
    Legendary,  // ����
    Key         // ���丮 ������ (�߰���)
}

// �������� ������ �����ϱ� ���� enum
public enum ItemType
{
    Weapon,
    Passive,
    Active,
    Consumable,
    Key
}

public enum WeaponType
{
    Sword,
    Bow,
    Hacking
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Game/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("�ٽ� �ĺ� ����")]
    public string itemID;
    public string itemName;
    public ItemType itemType;

    [Header("���� ǥ�� ����")]
    [TextArea] public string description;
    public Sprite itemIcon;
    public ItemRarity rarity;

    [Header("ȿ�� ��� ������ ������ (�нú�, ��Ƽ��, �Ҹ�ǰ)")]
    public List<ItemEffect> effects;

    [Header("��Ƽ�� ���� ������")]
    public float cooldown;
}