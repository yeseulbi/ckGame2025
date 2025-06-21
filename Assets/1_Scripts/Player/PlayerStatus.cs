using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : PlayerMove
{
    // level, hp, str, EP, exp, speed, AttackSpeed
    [Header("UI")]
    public Text HP;
    public Slider HpBar;
    
    public static PlayerStatus Instance { get; private set; }

    [Header("�ɷ�ġ")]
    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float str = 25f;          // ���ݷ�
    public float EP = 100f;               // ��ų ����Ʈ
    public float moveSpeed = 5f;          // �̵��ӵ� (������ ���)
    [Tooltip("���� ������ �ӵ�")] public float attackDelay = 0.5f;        // ���ݼӵ� (������ ���) -> ���ݵ����̼ӵ�

    [Tooltip("���� ����ġ")] public float currentExp = 0;
    [Tooltip("���� �������� ����ġ")] public float nextLevelExp = 100f;

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
            Debug.Log("�ִ� ���� ����");
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
