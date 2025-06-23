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

public enum SkillUnlockType
{
    None,
    DashInvincibility
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

    [Header("무기 전용 데이터")]
    public WeaponType weaponType;
    public float baseDamage;
    public float baseAttackDelay;
    public Vector2 attackPowerIncrease;
    public Vector2 skillDamageIncrease;
    public Vector2 uniqueStatIncrease;
    public Vector2 statusEffectChance;

    [Header("효과 기반 아이템 데이터 (패시브, 액티브, 소모품)")]
    public List<ItemEffect> effects;

    [Header("액티브 전용 데이터")]
    public float cooldown;

    [Header("스토리(Key) 아이템 전용 데이터")]
    public SkillUnlockType skillToUnlock;

    [HideInInspector]public StatusEffect statusEffect;  // 보유 상태이상 - 영웅 등급 이상 랜덤 추가
}