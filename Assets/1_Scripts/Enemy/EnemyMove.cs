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

    [Header("주변 순찰")]
    public bool patrol = true; // 주변 순찰 여부
    bool isMoving = false;

    [Header("히트박스 보이기")]
    public bool SeeHitbox = false;

    // 이동 방향: 1이면 오른쪽, -1이면 왼쪽
    protected float moveDir = 1f;

    // 플레이어를 발견 했을 때 추적 사거리 늘어남
    float DetectionRange;

    // 1회성 초기 리스폰 설정
    Vector3 Respawn;
    bool set = false;

    // 움직임 일시적 정지(벽 감지,행동 불가 등)에 사용
    protected bool canMove = true;
    // 공격 정지
    protected bool canAttack = true;

    // 플레이어 감지
    protected bool isInPlayer = false;

    // 사망
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

        // 플레이어 감지 레이
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            (playerPos - transform.position).normalized,
            DetectionRange,
            LayerMask.GetMask("Player", "Floor")
        );
        Debug.DrawRay(transform.position, (playerPos - transform.position).normalized * DetectionRange, Color.red);

        // 플레이어와 적 사이에 벽이 있는지 체크(위에 있는 플레이어 감지X)
        Vector3 rayOrigin = transform.position + Vector3.down * 0.8f;
        RaycastHit2D wallCheck = Physics2D.Raycast(
            rayOrigin,
            (playerPos+Vector3.down * 0.8f - rayOrigin).normalized * DetectionRange,
            Vector3.Distance(transform.position, playerPos),
            LayerMask.GetMask("Floor")
        );
        
        if (_Data.canNormalAttack)
        {
            // 플레이어가 공격 범위 안에 들어왔을 때 레이
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

        // 벽 감지 레이
        RaycastHit2D isWall = Physics2D.Raycast(
            transform.position,
            Vector3.right * moveDir,
            0.5f,
            LayerMask.GetMask("Floor")
        );
        Debug.DrawRay(transform.position, Vector3.right * moveDir * 0.5f, Color.cyan);

        // 코너 감지 레이 (대각선 아래 방향, 한 칸 앞)
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

        // 앞에 벽/낭떠러지 있음
        if ((isWall ||!isCorner))
        {
            moveDir *= -1f;     // moveDir은 Ray에 관여하기 때문에 먼저 반전하고, 코루틴을 사용, 이후에 스프라이트를 반전한다
            StartCoroutine(StopAndTurn());
        }

        // 플레이어가 감지 범위 내에 있고 벽이 없을 때 추적
        if (hit && hit.collider.CompareTag("Player") && wallCheck.collider == null&&canMove)
        {
            //Debug.Log("시야 내 플레이어 있음");
            speedbalance = 1f;
            DetectionRange = _Data.detectionRange * 2;

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
            transform.Translate(Vector3.right * (playerPos - transform.position).normalized.x * speedbalance * _Data.moveSpeed * Time.deltaTime, Space.World);
            isMoving = true;
        }
        else
        {
            DetectionRange = _Data.detectionRange;

            if (canMove)
            { 
                // 평상시 이동 (좌우 순찰)
                if (patrol)
                {
                    transform.Translate(Vector3.right * moveDir * _Data.moveSpeed * speedbalance * Time.deltaTime, Space.World);
                    isMoving = true;
                }
                // 순찰 아님
                else
                {
                    // 처음 스폰 장소로 돌아감
                    float stopDistance = 0.1f;
                    if (Vector3.Distance(transform.position, Respawn) > stopDistance)
                    {
                        // 이동
                        moveDir = Mathf.Sign((Respawn - transform.position).normalized.x);
                        transform.Translate(Vector3.right * moveDir * speedbalance * _Data.moveSpeed * Time.deltaTime, Space.World);
                    
                        Vector3 scale = transform.localScale;
                        scale.x = Mathf.Abs(scale.x) * Mathf.Sign((Respawn - transform.position).normalized.x);
                        transform.localScale = scale;

                        isMoving = true;
                    }
                    else
                    {
                        // 도착
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

        // 회전
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