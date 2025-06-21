using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHitBox : MonoBehaviour
{
    GameObject enemy;

    private void Awake()
    {
        enemy = transform.parent.gameObject; // 본인 오브젝트 가져오기
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatus player = collision.GetComponent<PlayerStatus>();  // 맞은 플레이어의 스탯 관리cs

            enemy.GetComponent<EnemyStatus>().Effect();

            player.GetComponent<Rigidbody2D>().AddForce((player.transform.position - enemy.transform.position).normalized * 0.1f, ForceMode2D.Impulse);
            /*힘을 주는 부분, 밀어내는 힘이 있다면 다를 것 같다. 0.1f부분 수정*/
        }
    }
    public IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false); // Hitbox 비활성화
    }
}
