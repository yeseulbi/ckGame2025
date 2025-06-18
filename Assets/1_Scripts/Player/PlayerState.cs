using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // level, hp, str, EP, exp, speed, AttackSpeed

    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float strength = 25f;          // 공격력
    public float EP = 100f;               // 스킬 포인트
    public float moveSpeed = 5f;          // 이동속도 (아이템 기반)
    public float attackSpeed = 3f;        // 공격속도 (아이템 기반)

    public float currentExp = 0;
    public float nextLevelExp = 100f;

    public void LevelUp()
    {
        if (level < 10)
        {
            level++;
            maxHp += 20f;
            currentHp = maxHp;
            EP += 10;
            strength += 5f;
            currentExp = 0;
            nextLevelExp *= 1.6f;
        }
        else
        {
            Debug.Log("최대 레벨 도달");
        }
    }
}
