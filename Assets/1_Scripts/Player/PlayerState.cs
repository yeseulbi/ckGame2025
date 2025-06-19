using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // level, hp, str, EP, exp, speed, AttackSpeed

    public static PlayerState Instance { get; private set; }

    [Header("능력치")]
    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float strength = 25f;          // 공격력
    public float EP = 100f;               // 스킬 포인트
    public float moveSpeed = 5f;          // 이동속도 (아이템 기반)
    [Tooltip("공격 딜레이 속도")] public float attackDelay = 0.5f;        // 공격속도 (아이템 기반) -> 공격딜레이속도

    [Tooltip("현재 경험치")] public float currentExp = 0;
    [Tooltip("다음 레벨까지 경험치")] public float nextLevelExp = 100f;

    private void Awake()
    {
        Instance = this;
    }
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

       /* MainStatus.StatusEffectAdd.Add(StatusEffect.Invincible, 10f);

        Debug.Log(MainStatus.StatusEffectAdd.ContainsKey((StatusEffect)1));*/
    }
}
