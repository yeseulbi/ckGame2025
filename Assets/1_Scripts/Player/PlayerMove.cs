using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("무기 공격 이펙트 프리팹")]
    public GameObject[] Weapon_Effect;

    [Header("플레이어 효과음")]
    public AudioClip[] Running; // 바닥마다 걷는 소리 다름
    public AudioClip Jump, Dash, Hurt, onFloor;

    [Header("점프 높이, 이동 속도")]
    public float force = 11;
    public float speed = 6f;


    public GameObject Weapon_HitBox;

    // 행동 상태
    bool onGround, isMoving;
    bool canAttack = true;

    Vector3 lookDirection = Vector2.right; // 바라보는 방향을 Vector2로 저장
    Animator Player_Anim, Weapon_Anim;
    Rigidbody2D rb;
    AudioSource audioSource;

    private void Awake()
    {
        Player_Anim = transform.GetChild(0).GetComponent<Animator>();
        Weapon_Anim = transform.GetChild(1).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        Player_Anim.speed = (1+speed/100f);

        // 좌우 이동 & 이동 애니메이션 상태 업데이트
        if (horizontal != 0)
        {
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

            // Sprite 방향 반전
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);

            lookDirection = new Vector3(Mathf.Sign(horizontal), 0); // 바라보는 방향 업데이트

            isMoving = true;
        }
        else
            isMoving = false;

        Player_Anim.SetFloat("moveStep", Mathf.Abs(horizontal));

        // 점프
        if (onGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.PlayOneShot(Jump);
            rb.AddForceY(force);
        }
        else if (rb.linearVelocityY > 0 && Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.linearVelocityY *= Input.GetAxis("Vertical");
        }
        // 공중 애니메이션 상태 업데이트
        if (!onGround)
        {
            if (rb.linearVelocityY > 0)
                Player_Anim.SetInteger("onSky", 1);
            else if (rb.linearVelocityY < 0)
                Player_Anim.SetInteger("onSky", -1);
        }
        else
        {
            Player_Anim.SetInteger("onSky", 0);
        }

        // 공격
        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            // 공격 HitBox 활성화
            Weapon_HitBox.SetActive(true);
            PlayerWeaponHitBox.Instance.entered = true; // 코루틴 활성화
            Weapon_Anim.SetTrigger("Attack_1");
            var effect = Instantiate(Weapon_Effect[0], lookDirection, Quaternion.identity, transform);

            AnimatorStateInfo EfectStateInfo = effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);  //애니메이션 상태 정보 가져오기
            Destroy(effect, EfectStateInfo.length); //애니메이션 길이만큼 지속 후 제거

            // 공격 무기 효과음 재생
            WeaponSet.Instance.WeaponSF_Play(0);

            canAttack = false;
            StartCoroutine(AttackDelay(0));
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            onGround = true;
            audioSource.PlayOneShot(onFloor);
        }
    }
        float currenttime,time = 0;
        bool onStepSF = true;
    private void OnCollisionStay2D(Collision2D other)
    {

        if (!onGround && other.gameObject.CompareTag("Floor"))
            onGround = true;

        if (isMoving)   // 발자국 소리 타이밍
        {
            currenttime = -time + Time.time;
            if (0.3f - speed / 100f < currenttime && onStepSF)
            {
                audioSource.clip = Running[0];
                audioSource.Play();
                onStepSF = false;
                time = Time.time;
            }
            if (!onStepSF)
            {
                currenttime = 0;
                onStepSF = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
    }

    IEnumerator AttackDelay(float AttackSpeed)  // 추가 어택 속도, 없으면 0
    {
        var Delay = PlayerState.Instance.attackDelay;
        yield return new WaitForSeconds(Delay - AttackSpeed / 100f * Delay);
        canAttack = true;
    }
}