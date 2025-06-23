using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("�̸� / ����")]
    public string enemyName;
    public EnemyType enemyType;

    [Header("�ɷ�ġ")]
    public float maxHP;
    public float moveSpeed;
    public float str;

    [Header("�Ϲ� ���� ����")]
    public bool canNormalAttack; // �Ϲ� ���� ��� ����
    public float detectionRange;  // �߰� ����
    public float attackCooltime;

    [Header("�Ϲ� ���� ����Ʈ / ȿ��")]
    public GameObject AttackFX;
    public AudioClip AttackSound;

    [Header("��ų")]
    public bool canUseSkill;    // ��ų ��� ����
    public SkillData[] skills; // ����ϴ� ��ų

    [Header("���� / ����"), Tooltip("���� ����, �����")]
    public Buff[] buffs;
    public Debuff[] debuffs;
    
    [Header("����")]
    public bool isBoss;
}

public enum EnemyType
{
    Melee,           // �ٰŸ�         // 3��
    Ranged,          // ���Ÿ�         // 3��
    AOE,             // ����           // 2��
    Supporter,       // ����          //  2��
    MiniBoss,        // �̴� ����      // 2��(�ٰŸ�1, ���Ÿ�1)
    Boss             // ����          // 3��(1����,2����,3����)
}
