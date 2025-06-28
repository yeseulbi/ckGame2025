using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public enum CharacterType
{
    Android,
    Human
}
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
    public float str = 10f;          // 공격력
    public float EP = 100f;               // 스킬 포인트
    public float moveSpeed = 5f;          // 이동속도 (아이템 기반)
    [Tooltip("공격 딜레이 속도")] public float attackDelay = 1f;        // 공격속도 -> 무기공격딜레이속도/공속

    [Tooltip("현재 경험치")] public float currentExp = 0;
    [Tooltip("다음 레벨까지 경험치")] public float nextLevelExp = 100f;

    public static bool DontGetDamage;   //무적
    WaitForSeconds invincibleTime = new WaitForSeconds(0.8f);        //무적시간(공격 받았을 때)
    public CharacterType characterType;
    public SkillData ultimateSkill;

    void Start()
    {
        switch (characterType)
        {
            case CharacterType.Android:
                //ultimateSkill = AndroidSkill;
                break;
            case CharacterType.Human:
                // ultimateSkill = HumanSkill;
                break;
        }
    }

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
            str += 10f;
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

    public IEnumerator TakeDamage(float str)    //데미지를 받은 후 0.8초 무적, 무적 시간엔 함수 무시
    {
        if(!DontGetDamage)
        {
            DontGetDamage = true;
            currentHp -= str;
            Player_Anim.SetTrigger("isHurt");
            audioSource.PlayOneShot(Hurt);

            spRenderer.color = Color.red;

            yield return invincibleTime;
                DontGetDamage = false;
            spRenderer.color = Color.white;
        }
    }

    public void myDamage()
    {
        
    }

    /*public float CalculateFinalDamage()
    {
        // 1. 장착한 무기의 정보를 가져옵니다.
        WeaponData equippedWeapon = inventory.GetEquippedWeapon();

        // 2. 캐릭터의 스탯 보너스를 계산합니다. (str 1 = 1% 가정)
        float statBonus = this.str * 0.01f;

        // 3. 최종 데미지를 계산합니다.
        float finalDamage = equippedWeapon.baseDamage * (1f + statBonus);

        // (심화) 여기에 무기 자체의 %옵션, 크리티컬, 버프 등을 추가로 계산할 수 있습니다.
        // finalDamage *= (1f + equippedWeapon.attackPowerIncrease.x * 0.01f);

        return finalDamage;
    }*/
}
