// FileName: ItemEffectSystem.cs
using UnityEngine;
public enum EffectTarget
{
    Self,           // 자신
    AllEnemies,     // 모든 적
    AreaAroundSelf  // 자신 주변 범위
}
public enum EffectType
{
    // 스탯 변경 (버프/디버프)
    ModifyStat,
    // 체력/자원 회복 및 감소
    Heal_HP,
    Heal_EP,
    // 상태이상 적용
    ApplyStatusEffect,
    // 모든 상태이상 제거
    CureAllStatusEffects,
    // 부활
    Revive
}
public enum StatType
{
    MoveSpeed,          // 이동속도
    EpRegenRate,        // EP 회복률
    DamageReduction,    // 받는 데미지 감소
    MaxHP,              // 최대 체력
    BaseAttack,         // 기본 공격력
    AttackSpeed,        // 공격 속도
    Targeting,          // 명중률
    RareItemDropRate,   // 희귀 아이템 드랍률
    CooldownReduction,  // 스킬 쿨타임 감소
    TotalAttack,        // 전체 공격력
    EnemyAttack         // (디버프용) 적 공격력
}

[System.Serializable]
public class ItemEffect
{
    [Tooltip("효과의 종류 (HP회복, 스탯변경, 상태이상 부여 등)")]
    public EffectType effectType;

    [Tooltip("효과가 적용될 대상 (자신, 모든 적 등)")]
    public EffectTarget target;

    [Header("스탯 변경 시")]
    [Tooltip("어떤 스탯을 변경할지 (effectType이 ModifyStat일 때만 사용)")]
    public StatType statToModify;

    [Header("상태이상 적용 시")]
    [Tooltip("어떤 상태이상을 적용할지 (effectType이 ApplyStatusEffect일 때만 사용)")]
    public StatusEffect statusToApply; // MainStatus.cs에 정의된 StatusEffect 열거형 사용

    [Header("효과 값")]
    [Tooltip("효과의 수치 (고정값 또는 퍼센트)")]
    public float value;
    [Tooltip("효과의 지속 시간 (0이면 즉시 또는 영구)")]
    public float duration;
    [Tooltip("value가 백분율(%) 단위인지 여부")]
    public bool isPercentage;
}