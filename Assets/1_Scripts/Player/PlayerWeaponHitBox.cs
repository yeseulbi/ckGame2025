using System.Collections;
using UnityEngine;

public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("Ÿ�� ����Ʈ ������")]
    public GameObject[] Attack_Effect;
    
    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;
    private void Awake()
    {
        Instance = this;
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
        if (collision.gameObject.CompareTag("Enemy")&&entered)
        {
            Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // Ÿ�� ����Ʈ �迭 Ȯ�� ����
        }
        gameObject.SetActive(false); // HitBox ��Ȱ��ȭ
    }
    /*private void Update()
    {
        if (transform.childCount == 0)
    }*/
    //������ �߰�

    IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        entered = false; // 0.1�� �Ŀ� entered�� false�� ����
        gameObject.SetActive(false); // HitBox ��Ȱ��ȭ
    }
}