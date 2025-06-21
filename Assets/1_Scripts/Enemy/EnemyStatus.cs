using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : EnemyMove
{
    // 체력
    float currentHp;

    // 체력바
    public GameObject Hpbar;    // 구조 변동 시 inspector연결 가능
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
        currentHp = Mathf.Max(0, currentHp); // 0 미만 방지

        float hpRatio = (_Data.maxHP > 0) ? currentHp / _Data.maxHP : 0f;
        hpRatio = Mathf.Clamp01(hpRatio); // 0~1로 제한

        currentHpBar.transform.localScale = new Vector3(hpRatio, 0.1f, 1);

        Debug.Log("자식의 moveDir: " + moveDir);
        Debug.Log("현재 체력: "+currentHp);
    }
    public void Effect()
    {
        var Obj = Instantiate(_Data.AttackFX, player.transform.position, Quaternion.identity); // 타격 이펙트 배열 확장 변경
        audioSource.Play();

        var particle = Obj.GetComponent<ParticleSystem>();
        float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // 파티클 시스템의 지속 시간과 + 시작 LifeTime 제거 시간 계산
        player.GetComponent<PlayerStatus>().TakeDamage(_Data.str);
        Destroy(Obj, RemoveTime);
    }
}