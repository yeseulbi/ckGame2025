using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public EnemyType enemyType;
    public float maxHP;
    public float moveSpeed;
    public float attackPower;
    public float detectionRange;
    public float attackRange;
    public float attackCooldown;

    public GameObject enemyPrefab;
    public SkillData[] skills; // 사용하는 스킬

    //public StatusEffect[] defaultStatusWeaknesses; // 예: 빙결 약함
    public bool isBoss;
}

public enum EnemyType
{
    Melee,
    Ranged,
    Summoner,
    Boss
}
