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
    public void WeaponDamageSet(WeaponItemData weapon)
    {

    }
    public float myDamage()
    {
        float damage = str + MyItemSet.Instance.myWeapon.baseDamage;
        return damage;
    }
    public float myAttackDelay()
    {
        float attackdelay = MyItemSet.Instance.myWeapon.baseAttackDelay;
        return attackdelay;
    }
}
