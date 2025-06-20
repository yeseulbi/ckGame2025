using System.Collections;
using UnityEngine;

public class attackEffectManager
{
    [Header("����Ʈ ��ƼŬ�ý��� ������")]
    public GameObject[] Effect_Prefabs;
    
}
public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("Ÿ�� ����Ʈ ��ƼŬ�ý��� ������")]
    public GameObject[] Attack_Effect;
    
    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;

    AudioSource audioSource;
    int Index; // ���⿡ ���� ����Ʈ ������ ���� �ε��� ����
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
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
        if (collision.gameObject.CompareTag("Enemy")&&entered)  // OnTriggerEnter�� �������� �ʴ� ���߿��� HitBox�� ������ ������ �ʴ� ���� �ذ�
        {
            var Obj = Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // Ÿ�� ����Ʈ �迭 Ȯ�� ����
            WeaponSet.Instance.AttackSF_Play(0);

            GameObject player = transform.parent.gameObject; // �÷��̾� ������Ʈ ��������
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - player.transform.position).normalized *2.5f , ForceMode2D.Impulse);
            /*������ ���� �ִ� �κ�, �о�� ���� �ִٸ� ���⺰�� �ٸ� �� ����. 2.5f�κ� ����*/
            var particle = Obj.GetComponent<ParticleSystem>();
            float RemoveTime = particle.main.duration + particle.main.startLifetime.constantMax;    // ��ƼŬ �ý����� ���� �ð��� + ���� LifeTime ���� �ð� ���

            Destroy(Obj, RemoveTime);
        }
        gameObject.SetActive(false); // HitBox ��Ȱ��ȭ
    }
    //������ �߰�, PlayerStat.cs���� ���� ��������

    IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        entered = false; // 0.1�� �Ŀ� entered�� false�� ����
        gameObject.SetActive(false); // HitBox ��Ȱ��ȭ
    }
}