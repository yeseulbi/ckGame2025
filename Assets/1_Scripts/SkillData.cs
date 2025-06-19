using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public SkillType skillType;

    [TextArea]
    public string description;

    public float epCost;
    public float cooldown;
    public float damage;

    public float range;
    public float effectRadius;

    public GameObject skillEffectPrefab;
    //public StatusEffect statusEffectInflicted; // 부여하는 상태이상 (예: 독, 화상 등)

    public AudioClip soundEffect;
}

public enum SkillType
{
    Attack,
    Defense,
    Utility,
    Ultimate
}
