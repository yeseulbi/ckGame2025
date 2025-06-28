using UnityEngine;

public class StatusEffectData
{
    public StatusEffect effect;  // ex) Burn, Frozen ��
    public float duration;       // ex) 5��, 3�� ��
}
[System.Serializable]
public class BuffData
{
    public Buff buff;            // ex) ���ݷ� ����
    public float value;          // ex) +20% ��
    public float duration;       // ex) 5��
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
