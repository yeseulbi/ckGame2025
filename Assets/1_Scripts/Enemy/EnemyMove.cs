using UnityEngine;
using UnityEngine.UIElements;

/*d
 
 
 */
public class EnemyMove : MonoBehaviour
{
    EnemyData enemy_Data;
    Animator animator;
    GameObject player;
    private void Awake()
    {
        enemy_Data = GetComponent<EnemyData>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }
    void Start()
    {

    }

    void FixedUpdate()
    {
        var currentPos = transform.position;
        Vector3 playerPos = player.transform.position;

        // enemyData.detectionRange가 감지 범위 반지름, 방향은 본인->플레이어 기준, Player 태그의 콜라이더가 들어오면 인식
        RaycastHit2D hit = Physics2D.Raycast(currentPos, playerPos - currentPos, enemy_Data.detectionRange, LayerMask.GetMask("Effect"));

        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("시야 내 플레이어 있음");
            MoveToPlayer();        
        }
    }

    /// <summary>
    /// enemy가 플레이어를 추적하기 위한 함수. 플레이어의 방향을 찾아 경로를 설계한다.
    /// </summary>
    void MoveToPlayer()
    {
        if (player != null)
        {
            /*레이캐스트에 벽이 있다면 vector x 방향으로 */

            transform.Translate(player.transform.position.normalized * Time.fixedDeltaTime * enemy_Data.moveSpeed); // 경로 설계 전
        }
    }
}
