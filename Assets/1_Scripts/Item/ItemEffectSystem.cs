// FileName: ItemEffectSystem.cs
using UnityEngine;
public enum EffectTarget
{
    Self,           // �ڽ�
    AllEnemies,     // ��� ��
    AreaAroundSelf  // �ڽ� �ֺ� ����
}
public enum EffectType
{
    // ���� ���� (����/�����)
    ModifyStat,
    // ü��/�ڿ� ȸ�� �� ����
    Heal_HP,
    Heal_EP,
    // �����̻� ����
    ApplyStatusEffect,
    // ��� �����̻� ����
    CureAllStatusEffects,
    // ��Ȱ
    Revive
}
public enum StatType
{
    MoveSpeed,          // �̵��ӵ�
    EpRegenRate,        // EP ȸ����
    DamageReduction,    // �޴� ������ ����
    MaxHP,              // �ִ� ü��
    BaseAttack,         // �⺻ ���ݷ�
    AttackSpeed,        // ���� �ӵ�
    Targeting,          // ���߷�
    RareItemDropRate,   // ��� ������ �����
    CooldownReduction,  // ��ų ��Ÿ�� ����
    TotalAttack,        // ��ü ���ݷ�
    EnemyAttack         // (�������) �� ���ݷ�
}

[System.Serializable]
public class ItemEffect
{
    [Tooltip("ȿ���� ���� (HPȸ��, ���Ⱥ���, �����̻� �ο� ��)")]
    public EffectType effectType;

    [Tooltip("ȿ���� ����� ��� (�ڽ�, ��� �� ��)")]
    public EffectTarget target;

    [Header("���� ���� ��")]
    [Tooltip("� ������ �������� (effectType�� ModifyStat�� ���� ���)")]
    public StatType statToModify;

    [Header("�����̻� ���� ��")]
    [Tooltip("� �����̻��� �������� (effectType�� ApplyStatusEffect�� ���� ���)")]
    public StatusEffect statusToApply; // MainStatus.cs�� ���ǵ� StatusEffect ������ ���

    [Header("ȿ�� ��")]
    [Tooltip("ȿ���� ��ġ (������ �Ǵ� �ۼ�Ʈ)")]
    public float value;
    [Tooltip("ȿ���� ���� �ð� (0�̸� ��� �Ǵ� ����)")]
    public float duration;
    [Tooltip("value�� �����(%) �������� ����")]
    public bool isPercentage;
}