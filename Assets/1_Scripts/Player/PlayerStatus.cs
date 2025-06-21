using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : PlayerMove
{
    // level, hp, str, EP, exp, speed, AttackSpeed
    [Header("UI")]
    public Text HP;
    public Slider HpBar;
    
    public static PlayerStatus Instance { get; private set; }

    [Header("능력치")]
    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float str = 25f;          // 공격력
    public float EP = 100f;               // 스킬 포인트
    public float moveSpeed = 5f;          // 이동속도 (아이템 기반)
    [Tooltip("공격 딜레이 속도")] public float attackDelay = 0.5f;        // 공격속도 (아이템 기반) -> 공격딜레이속도

    [Tooltip("현재 경험치")] public float currentExp = 0;
    [Tooltip("다음 레벨까지 경험치")] public float nextLevelExp = 100f;

    public override void Awake()
    {
        base.Awake();
        Instance = this;

        currentHp = maxHp;
    }
    public void LevelUp()
    {
        if (level < 10)
        {
            level++;
            maxHp += 20f;
            currentHp = maxHp;
            EP += 10;
            str += 5f;
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
    public override void Update()
    {
        base.Update();
        HpBar.value = currentHp;
        HP.text = currentHp.ToString();
    }

    public void TakeDamage(float str)
    {
        currentHp -= str;
        Player_Anim.SetTrigger("isHurt");
        audioSource.PlayOneShot(Hurt);
    }
}
