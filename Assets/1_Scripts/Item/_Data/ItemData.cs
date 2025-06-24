// FileName: ItemData.cs
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 아이템 등급을 정의하는 열거형입니다.
/// </summary>
public enum ItemRarity
{
    Common,     // 일반
    Rare,       // 희귀
    Heroic,     // 영웅
    Legendary,  // 전설
    Key         // 스토리 아이템 (추가됨)
}

// 아이템의 종류를 구분하기 위한 enum
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
    [Header("핵심 식별 정보")]
    public string itemID;
    public string itemName;
    public ItemType itemType;

    [Header("공통 표시 정보")]
    [TextArea] public string description;
    public Sprite itemIcon;
    public ItemRarity rarity;

    [Header("효과 기반 아이템 데이터 (패시브, 액티브, 소모품)")]
    public List<ItemEffect> effects;

    [Header("액티브 전용 데이터")]
    public float cooldown;
}