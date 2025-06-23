using System.Collections;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public EnemyData _Data;
    protected GameObject player;
    public GameObject Hitbox;
    protected Animator anim;
    protected AudioSource audioSource;
    SpriteRenderer HitboxSr;

    [Header("�ֺ� ����")]
    public bool patrol = true; // �ֺ� ���� ����
    bool isMoving = false;

    [Header("��Ʈ�ڽ� ���̱�")]
    public bool SeeHitbox = false;

    // �̵� ����: 1�̸� ������, -1�̸� ����
    protected float moveDir = 1f;

    // �÷��̾ �߰� ���� �� ���� ��Ÿ� �þ
    float DetectionRange;

    // 1ȸ�� �ʱ� ������ ����
    Vector3 Respawn;
    bool set = false;

    // ������ �Ͻ��� ����(�� ����,�ൿ �Ұ� ��)�� ���
    protected bool canMove = true;
    // ���� ����
    protected bool canAttack = true;

    // �÷��̾� ����
    protected bool isInPlayer = false;

    // ���
    protected bool hp0_Dead = false;

    public virtual void Awake()
    {
        player = GameObject.Find("Player");
        Hitbox = transform.GetChild(2).gameObject;
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        HitboxSr = Hitbox.GetComponent<SpriteRenderer>();

        DetectionRange = _Data.detectionRange;
        Respawn = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !set)
        {
            Respawn = transform.position;
            set = true;
        }
    }
    public virtual void Update()
    {
        HitboxSr.enabled = SeeHitbox;
        isInPlayer = false;

        if (hp0_Dead)
        {
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<Collider2D>());
            canMove = false;

            StartCoroutine(DeadMotion());
            return;
        }

        Vector3 playerPos = player.transform.position;

        // �÷��̾� ���� ����
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (playerPos - transform.position).normalized,
            DetectionRange,
            LayerMask.GetMask("Player", "Floor")
        );
        Debug.DrawRay(transform.position, (playerPos - transform.position).normalized * DetectionRange, Color.red);

        // �÷��̾�� �� ���̿� ���� �ִ��� üũ(���� �ִ� �÷��̾� ����X)
        Vector3 rayOrigin = transform.position + Vector3.down * 0.8f;
        RaycastHit2D wallCheck = Physics2D.Raycast(
            rayOrigin,
            (playerPos+Vector3.down * 0.8f - rayOrigin).normalized * DetectionRange,
            Vector3.Distance(transform.position, playerPos),
            LayerMask.GetMask("Floor")
        );
        
        if (_Data.canNormalAttack)
        {
            // �÷��̾ ���� ���� �ȿ� ������ �� ����
            Vector3 posX = new Vector3(Mathf.Sign((playerPos - transform.position).normalized.x), 0f);
            RaycastHit2D attack = Physics2D.Raycast(
                transform.position,
                posX,
                Hitbox.transform.localScale.x-0.3f,
                LayerMask.GetMask("Player","Floor")
            );
            Debug.DrawRay(transform.position, posX, Color.magenta, Hitbox.transform.localScale.x-0.3f);

            if(attack&&wallCheck.collider==null)
                isInPlayer = true;
        }

        Debug.DrawRay(rayOrigin, (playerPos + Vector3.down * 0.8f - rayOrigin).normalized * DetectionRange, Color.green);

        // �� ���� ����
        RaycastHit2D isWall = Physics2D.Raycast(
            transform.position,
            Vector3.right * moveDir,
            0.5f,
            LayerMask.GetMask("Floor")
        );
        Debug.DrawRay(transform.position, Vector3.right * moveDir * 0.5f, Color.cyan);

        // �ڳ� ���� ���� (�밢�� �Ʒ� ����, �� ĭ ��)
        Vector3 cornerRayOrigin = transform.position + Vector3.right * moveDir * 0.5f;
        Vector2 cornerRayDir = new Vector2(moveDir, -1).normalized;
        float cornerRayLength = 3f;

        RaycastHit2D isCorner = Physics2D.Raycast(
            cornerRayOrigin,
            cornerRayDir,
            cornerRayLength,
            LayerMask.GetMask("Floor")
        );
        Debug.DrawRay(cornerRayOrigin, cornerRayDir * cornerRayLength, Color.yellow);

        float speedbalance = 0.8f;

        // �տ� ��/�������� ����
        if ((isWall ||!isCorner))
        {
            moveDir *= -1f;     // moveDir�� Ray�� �����ϱ� ������ ���� �����ϰ�, �ڷ�ƾ�� ���, ���Ŀ� ��������Ʈ�� �����Ѵ�
            StartCoroutine(StopAndTurn());
        }

        // �÷��̾ ���� ���� ���� �ְ� ���� ���� �� ����
        if (hit && hit.collider.CompareTag("Player") && wallCheck.collider == null&&canMove)
        {
            //Debug.Log("�þ� �� �÷��̾� ����");
            speedbalance = 1f;
            DetectionRange = _Data.detectionRange * 2;

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
            transform.Translate(Vector3.right * (playerPos - transform.position).normalized.x * speedbalance * _Data.moveSpeed * Time.deltaTime, Space.World);
            isMoving = true;
        }
        else
        {
            DetectionRange = _Data.detectionRange;

            if (canMove)
            { 
                // ���� �̵� (�¿� ����)
                if (patrol)
                {
                    transform.Translate(Vector3.right * moveDir * _Data.moveSpeed * speedbalance * Time.deltaTime, Space.World);
                    isMoving = true;
                }
                // ���� �ƴ�
                else
                {
                    // ó�� ���� ��ҷ� ���ư�
                    float stopDistance = 0.1f;
                    if (Vector3.Distance(transform.position, Respawn) > stopDistance)
                    {
                        // �̵�
                        moveDir = Mathf.Sign((Respawn - transform.position).normalized.x);
                        transform.Translate(Vector3.right * moveDir * speedbalance * _Data.moveSpeed * Time.deltaTime, Space.World);
                    
                        Vector3 scale = transform.localScale;
                        scale.x = Mathf.Abs(scale.x) * Mathf.Sign((Respawn - transform.position).normalized.x);
                        transform.localScale = scale;

                        isMoving = true;
                    }
                    else
                    {
                        // ����
                        isMoving = false;
                    }
                }
                Hitbox.SetActive(false);
            }
        }
        anim.SetBool("isWalk",isMoving);
    }

    IEnumerator StopAndTurn()
    {
        canMove = false;
        isMoving = false;

        yield return new WaitForSeconds(1.0f);
        canMove = true;

        // ȸ��
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * moveDir;
        transform.localScale = scale;
    }
    IEnumerator DeadMotion()
    {
        anim.SetBool("isDead", true);
        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Dead"));
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}