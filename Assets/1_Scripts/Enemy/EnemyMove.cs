using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemyData enemy_Data;
    Animator animator;
    GameObject player;

    // 이동 방향: 1이면 오른쪽, -1이면 왼쪽
    float moveDir = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;

        // 플레이어 추적
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (playerPos - transform.position).normalized,
            enemy_Data.detectionRange,
            LayerMask.GetMask("Player", "Floor")
        );
        Debug.DrawRay(transform.position, (playerPos - transform.position).normalized * enemy_Data.detectionRange, Color.red);

        // 벽 감지
        RaycastHit2D isWall = Physics2D.Raycast(
            transform.position,
            Vector3.right * moveDir,
            0.5f,
            LayerMask.GetMask("Floor")
        );
        Debug.DrawRay(transform.position, Vector3.right * moveDir * 0.5f, Color.cyan);

        float speedbalance = 0.8f;

        // 앞에 벽 있음
        if (isWall && isWall.collider.CompareTag("Floor"))
        {
            moveDir *= -1f;
            
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * moveDir;
            transform.localScale = scale;
        }

        // 플레이어가 감지 범위 내에 있으면 추적
        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("시야 내 플레이어 있음");
            speedbalance = 1f;

            // 플레이어 방향 바라보기 (스프라이트 반전)
            float playerDir = (player.transform.position - transform.position).normalized.x;
            if (playerDir != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * Mathf.Sign(playerDir);
                transform.localScale = scale;
                moveDir = Mathf.Sign(playerDir);
            }

            // 플레이어를 향해 이동
            transform.Translate(Vector3.right * (playerPos - transform.position).normalized.x * speedbalance * enemy_Data.moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // 평상시 이동 (좌우 순찰)
            transform.Translate(Vector3.right * moveDir * enemy_Data.moveSpeed * speedbalance * Time.deltaTime, Space.World);
        }
    }
}