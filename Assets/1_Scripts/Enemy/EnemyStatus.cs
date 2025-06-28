using System.Collections;
using UnityEngine;

public class EnemyStatus : EnemyMove
{
    // ü��
    float currentHp;

    // ü�¹�
    public GameObject Hpbar;    // ���� ���� �� inspector���� ����

    public Rigidbody2D rb2D;
    GameObject currentHpBar;
    SpriteRenderer sr;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!PlayerStatus.DontGetDamage)
        {
            StartCoroutine(player.GetComponent<PlayerStatus>().TakeDamage(_Data.str));
            Effect();
        }
    }
    public override void Awake()
    {
        base.Awake();
        if (Hitbox == null)
            Hitbox = transform.GetChild(2).gameObject;
        if (Hpbar == null)
            Hpbar = transform.GetChild(0).gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(1).GetComponent<SpriteRenderer>();
        currentHpBar = Hpbar.transform.GetChild(0).gameObject;

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

        if (Hpbar.transform.localScale.x != transform.localScale.x)
             Hpbar.transform.localScale = new Vector3(transform.localScale.x, Hpbar.transform.localScale.y, Hpbar.transform.localScale.z);

        if (canAttack && isInPlayer && anim.GetInteger("Attack") == 0 && !hp0_Dead)
        {
            canAttack = false;
            StartCoroutine(Attacked(_Data.enemyType));
        }
    }

    public void TakeDamage()
    {

        StartCoroutine(HitColor());
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
        var Obj = Instantiate(_Data.AttackFX, player.transform.position, Quaternion.identity); // Ÿ�� ����Ʈ
        audioSource.Play();

        var particle = Obj.GetComponent<ParticleSystem>();
        float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // ��ƼŬ �ý����� ���� �ð��� + ���� LifeTime ���� �ð� ���
        StartCoroutine(player.GetComponent<PlayerStatus>().TakeDamage(_Data.str));
        Destroy(Obj, RemoveTime);
    }

    WaitForSeconds Seconds03 = new WaitForSeconds(0.3f);
    IEnumerator HitColor()
    {
        sr.color = Color.red;
        yield return Seconds03;
        sr.color = Color.white;
    }
    IEnumerator Attacked(EnemyType type)
    {
        anim.SetInteger("Attack", 1);

        canMove = false;
        yield return new WaitUntil(() =>
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
        anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_0"));

        switch (type)
        {
            case EnemyType.Melee:
                Hitbox.SetActive(true);
                StartCoroutine(Hitbox.transform.GetComponent<EnemyHitBox>().CheckTriggerTimeout());
                break;
            case EnemyType.Ranged:
                //����ü �߻�
                break;
        }

        yield return new WaitUntil(() =>
        anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f &&
        anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1"));

        anim.SetInteger("Attack", 0);

        canMove = true;

        yield return new WaitForSeconds(_Data.attackCooltime);
        canAttack = true;
    }
}