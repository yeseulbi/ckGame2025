using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : EnemyMove
{
    // ü��
    float currentHp;

    // ü�¹�
    public GameObject Hpbar;    // ���� ���� �� inspector���� ����
    GameObject currentHpBar;

    private Vector3 initialLocalScale;

    bool canAttack = true;
    public override void Awake()
    {
        base.Awake();
        if (Hitbox == null)
            Hitbox = transform.GetChild(2).gameObject;
        if (Hpbar == null)
            Hpbar = transform.GetChild(0).gameObject;

        currentHpBar = Hpbar.transform.GetChild(0).gameObject;
        initialLocalScale = Hpbar.transform.localScale;
    }

    private void Start()
    {
        currentHp = _Data.maxHP;
        audioSource.clip = _Data.AttackSound;
    }

    public override void Update()
    {
        base.Update();

        if (currentHp <= 0)
        {
            hp0_Dead = true;
            Hitbox.SetActive(false);
        }

        Hpbar.transform.localScale = transform.localScale.x != 1 ? new Vector3(initialLocalScale.x * - 1, initialLocalScale.y, 1)
            : new Vector3(initialLocalScale.x, initialLocalScale.y, 1);

        if (canAttack&&isInPlayer && anim.GetInteger("Attack")==0&&!hp0_Dead)
        {
            canAttack = false;
            StartCoroutine(Attacked());
        }
    }
    
    IEnumerator Attacked()
    {
        anim.SetInteger("Attack", 1);

        canMove = false;
        yield return new WaitUntil(() =>
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
        anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"));

        Hitbox.SetActive(true);
        StartCoroutine(Hitbox.transform.GetComponent<EnemyHitBox>().CheckTriggerTimeout());

        yield return new WaitUntil(() =>
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
        anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"));

        anim.SetInteger("Attack", 0);

        canMove = true;

        yield return new WaitForSeconds(_Data.attackCooltime);
        canAttack = true;
    }

    public void TakeDamage()
    {
        anim.SetTrigger("isHit");
        currentHp -= PlayerStatus.Instance.str;
        currentHp = Mathf.Max(0, currentHp); // 0 �̸� ����

        float hpRatio = (_Data.maxHP > 0) ? currentHp / _Data.maxHP : 0f;
        hpRatio = Mathf.Clamp01(hpRatio); // 0~1�� ����

        currentHpBar.transform.localScale = new Vector3(hpRatio, 0.1f, 1);

        Debug.Log("�ڽ��� moveDir: " + moveDir);
        Debug.Log("���� ü��: "+currentHp);
    }
    public void Effect()
    {
        var Obj = Instantiate(_Data.AttackFX, player.transform.position, Quaternion.identity); // Ÿ�� ����Ʈ �迭 Ȯ�� ����
        audioSource.Play();

        var particle = Obj.GetComponent<ParticleSystem>();
        float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // ��ƼŬ �ý����� ���� �ð��� + ���� LifeTime ���� �ð� ���
        player.GetComponent<PlayerStatus>().TakeDamage(_Data.str);
        Destroy(Obj, RemoveTime);
    }
}