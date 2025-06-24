using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponItemData", menuName = "Game/WeaponItemData")]
public class WeaponItemData : ItemData
{
    [Header("무기 전용 데이터")]
    public WeaponType weaponType;
    public float baseDamage;
    public float baseAttackDelay;
    public Vector2 attackPowerIncrease;
    public Vector2 uniqueStatIncrease;
    public Vector2 statusEffectChance;

    [HideInInspector] public StatusEffect statusEffect;  // 보유 상태이상 - 영웅 등급 이상 랜덤 추가
    [HideInInspector] public float statusEffectPercent; //  상태이상 발동확률
}