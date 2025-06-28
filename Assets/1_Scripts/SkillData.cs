using UnityEngine;

public class StatusEffectData
{
    public StatusEffect effect;  // ex) Burn, Frozen 등
    public float duration;       // ex) 5초, 3초 등
}
[System.Serializable]
public class BuffData
{
    public Buff buff;            // ex) 공격력 증가
    public float value;          // ex) +20% 등
    public float duration;       // ex) 5초
}

[System.Serializable]
public class DebuffData
{
    public Debuff debuff;
    public float value;
    public float duration;
}
[CreateAssetMenu(fileName = "NewSkillData", menuName = "Game/Skill Data")]
[System.Serializable]
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
    public StatusEffect[] statusEffects;
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
