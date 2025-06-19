using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("스킬명 / 종류")]
    public string skillName;
    public SkillType skillType;
    
    [TextArea]
    public string description;

    [Header("스킬 스탯")]
    public float epCost;
    public float cooltime;
    public bool DoT; // 도트딜, 틱데미지
    public float damage;

    [Header("범위")]
    public float range;
    public float effectRadius;

    [Header("상태 이상 / 버프 / 디버프"), Tooltip("맞는 상대에게 부여하는 효과")]
    public StatusEffect statusEffect;
    public Buff[] buffs;
    public Debuff[] debuffs;

    public GameObject skillEffectPrefab;
    public AudioClip soundEffect;
}

public enum SkillType
{
    Attack,
    Defense,
    Utility,
    Ultimate
}
