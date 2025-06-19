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
    //public StatusEffect statusEffectInflicted; // �ο��ϴ� �����̻� (��: ��, ȭ�� ��)

    public AudioClip soundEffect;
}

public enum SkillType
{
    Attack,
    Defense,
    Utility,
    Ultimate
}
