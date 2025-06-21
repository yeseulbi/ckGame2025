using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHitBox : MonoBehaviour
{
    GameObject enemy;

    private void Awake()
    {
        enemy = transform.parent.gameObject; // ���� ������Ʈ ��������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatus player = collision.GetComponent<PlayerStatus>();  // ���� �÷��̾��� ���� ����cs

            enemy.GetComponent<EnemyStatus>().Effect();

            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - enemy.transform.position).normalized * 0.1f, ForceMode2D.Impulse);
            /*���� �ִ� �κ�, �о�� ���� �ִٸ� �ٸ� �� ����. 0.1f�κ� ����*/
        }
    }
    public IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false); // Hitbox ��Ȱ��ȭ
    }
}
