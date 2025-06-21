using System.Collections;
using UnityEngine;

public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("Ÿ�� ����Ʈ ��ƼŬ�ý��� ������")]
    public GameObject[] Attack_Effect;

    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;

    int Index; // ���⿡ ���� ����Ʈ ������ ���� �ε��� ����
    private void Awake()
    {
        Instance = this;
        //Index = ���⿡ ���� ����Ʈ ����. �ε����� �迭�� ���忡 ���� 
    }
    private void Update()
    {
        if (entered)
        {
            StartCoroutine(CheckTriggerTimeout()); // �ڷ�ƾ ����
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")&& entered)  // OnTriggerEnter�� �������� �ʴ� ���߿��� HitBox�� ������ ������ �ʴ� ���� �ذ�
        {
            EnemyStatus enemy = collision.GetComponent<EnemyStatus>();  // ���� ���� ���� ����cs
            GameObject player = transform.parent.gameObject; // �÷��̾� ������Ʈ ��������

            var Obj = Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // Ÿ�� ����Ʈ �迭 Ȯ�� ����
            WeaponSet.Instance.AttackSF_Play(0);


            enemy.TakeDamage();
            enemy.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - player.transform.position).normalized *2f , ForceMode2D.Impulse);
            /*������ ���� �ִ� �κ�, �о�� ���� �ִٸ� ���⺰�� �ٸ� �� ����. 2f�κ� ����*/
            var particle = Obj.GetComponent<ParticleSystem>();
            float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // ��ƼŬ �ý����� ���� �ð��� + ���� LifeTime ���� �ð� ���

            Destroy(Obj, RemoveTime);
        }
        gameObject.SetActive(false); // Hitbox ��Ȱ��ȭ
    }
    //������ �߰�, PlayerStat.cs���� ���� ��������

    IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        entered = false; // 0.1�� �Ŀ� entered�� false�� ����
        gameObject.SetActive(false); // Hitbox ��Ȱ��ȭ
    }
}