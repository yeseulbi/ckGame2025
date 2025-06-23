using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("이름 / 종류")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("능력치")]
    public float maxHP;
    public float moveSpeed;
    public float str;

    [Header("일반 공격 스탯")]
    public bool canNormalAttack; // 일반 공격 사용 여부
    public float detectionRange;  // 발각 범위
    public float attackCooltime;

    [Header("일반 공격 이펙트 / 효과")]
    public GameObject AttackFX;
    public AudioClip AttackSound;

    [Header("스킬")]
    public bool canUseSkill;    // 스킬 사용 여부
    public SkillData[] skills; // 사용하는 스킬

    [Header("저항 / 약점"), Tooltip("영구 버프, 디버프")]
    public Buff[] buffs;
    public Debuff[] debuffs;
    
    [Header("보스")]
    public bool isBoss;
}

public enum EnemyType
{
    Melee,           // 근거리         // 3종
    Ranged,          // 원거리         // 3종
    AOE,             // 광역           // 2종
    Supporter,       // 지원          //  2종
    MiniBoss,        // 미니 보스      // 2종(근거리1, 원거리1)
    Boss             // 보스          // 3종(1스테,2스테,3스테)
}
