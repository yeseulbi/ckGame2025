using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "Game/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("��ų�� / ����")]
    public string skillName;
    public SkillType skillType;
    
    [TextArea]
    public string description;

    [Header("��ų ����")]
    public float epCost;
    public float cooltime;
    public bool DoT; // ��Ʈ��, ƽ������
    public float damage;

    [Header("����")]
    public float range;
    public float effectRadius;

    [Header("���� �̻� / ���� / �����"), Tooltip("�´� ��뿡�� �ο��ϴ� ȿ��")]
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
