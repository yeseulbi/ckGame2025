using System.Collections;
using UnityEngine;

public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("타격 이펙트 파티클시스템 프리팹")]
    public GameObject[] Attack_Effect;

    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;

    int Index; // 무기에 따라 이펙트 변경을 위한 인덱스 변수
    private void Awake()
    {
        Instance = this;
        //Index = 무기에 따라 이펙트 변경. 인덱스를 배열과 사운드에 넣음 
    }
    private void Update()
    {
        if (entered)
        {
            StartCoroutine(CheckTriggerTimeout()); // 코루틴 시작
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&& entered)  // OnTriggerEnter이 감지되지 않는 공중에서 HitBox가 열리면 닫히지 않는 문제 해결
        {
            EnemyStatus enemy = collision.GetComponent<EnemyStatus>();  // 맞은 적의 스탭 관리cs
            GameObject player = transform.parent.gameObject; // 플레이어 오브젝트 가져오기

            var Obj = Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // 타격 이펙트 배열 확장 변경
            WeaponSet.Instance.AttackSF_Play(0);


            enemy.TakeDamage();
            enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - player.transform.position).normalized *2f , ForceMode2D.Impulse);
            /*적에게 힘을 주는 부분, 밀어내는 힘이 있다면 무기별로 다를 것 같다. 2f부분 수정*/
            var particle = Obj.GetComponent<ParticleSystem>();
            float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // 파티클 시스템의 지속 시간과 + 시작 LifeTime 제거 시간 계산

            Destroy(Obj, RemoveTime);
        }
        gameObject.SetActive(false); // Hitbox 비활성화
    }
    //데미지 추가, PlayerStat.cs에서 스탯 가져오기

    IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        entered = false; // 0.1초 후에 entered를 false로 설정
        gameObject.SetActive(false); // Hitbox 비활성화
    }
}