using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemyData enemy_Data;
    Animator animator;
    GameObject player;

    // �̵� ����: 1�̸� ������, -1�̸� ����
    float moveDir = 1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;

        // �÷��̾� ����
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (playerPos - transform.position).normalized,
            enemy_Data.detectionRange,
            LayerMask.GetMask("Player", "Floor")
        );
        Debug.DrawRay(transform.position, (playerPos - transform.position).normalized * enemy_Data.detectionRange, Color.red);

        // �� ����
        RaycastHit2D isWall = Physics2D.Raycast(
            transform.position,
            Vector3.right * moveDir,
            0.5f,
            LayerMask.GetMask("Floor")
        );
        Debug.DrawRay(transform.position, Vector3.right * moveDir * 0.5f, Color.cyan);

        float speedbalance = 0.8f;

        // �տ� �� ����
        if (isWall && isWall.collider.CompareTag("Floor"))
        {
            moveDir *= -1f;
            
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * moveDir;
            transform.localScale = scale;
        }

        // �÷��̾ ���� ���� ���� ������ ����
        if (hit && hit.collider.CompareTag("Player"))
        {
            Debug.Log("�þ� �� �÷��̾� ����");
            speedbalance = 1f;

            // �÷��̾� ���� �ٶ󺸱� (��������Ʈ ����)
            float playerDir = (player.transform.position - transform.position).normalized.x;
            if (playerDir != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * Mathf.Sign(playerDir);
                transform.localScale = scale;
                moveDir = Mathf.Sign(playerDir);
            }

            // �÷��̾ ���� �̵�
            transform.Translate(Vector3.right * (playerPos - transform.position).normalized.x * speedbalance * enemy_Data.moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // ���� �̵� (�¿� ����)
            transform.Translate(Vector3.right * moveDir * enemy_Data.moveSpeed * speedbalance * Time.deltaTime, Space.World);
        }
    }
}