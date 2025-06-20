using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("���� ���� ����Ʈ ������")]
    public GameObject[] Weapon_Effect;

    [Header("�÷��̾� ȿ����")]
    public AudioClip[] Running; // �ٴڸ��� �ȴ� �Ҹ� �ٸ�
    public AudioClip Jump, Dash, Hurt, onFloor;

    [Header("���� ����, �̵� �ӵ�")]
    public float force = 11;
    public float speed = 6f;


    public GameObject Weapon_HitBox;

    // �ൿ ����
    bool onGround, isMoving;
    bool canAttack = true;

    Vector3 lookDirection = Vector2.right; // �ٶ󺸴� ������ Vector2�� ����
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

        // �¿� �̵� & �̵� �ִϸ��̼� ���� ������Ʈ
        if (horizontal != 0)
        {
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

            // Sprite ���� ����
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);

            lookDirection = new Vector3(Mathf.Sign(horizontal), 0); // �ٶ󺸴� ���� ������Ʈ

            isMoving = true;
        }
        else
            isMoving = false;

        Player_Anim.SetFloat("moveStep", Mathf.Abs(horizontal));

        // ����
        if (onGround && Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.PlayOneShot(Jump);
            rb.AddForceY(force);
        }
        else if (rb.linearVelocityY > 0 && Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.linearVelocityY *= Input.GetAxis("Vertical");
        }
        // ���� �ִϸ��̼� ���� ������Ʈ
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

        // ����
        if (Input.GetKeyDown(KeyCode.X) && canAttack)
        {
            // ���� HitBox Ȱ��ȭ
            Weapon_HitBox.SetActive(true);
            PlayerWeaponHitBox.Instance.entered = true; // �ڷ�ƾ Ȱ��ȭ
            Weapon_Anim.SetTrigger("Attack_1");
            var effect = Instantiate(Weapon_Effect[0], lookDirection, Quaternion.identity, transform);

            AnimatorStateInfo EfectStateInfo = effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);  //�ִϸ��̼� ���� ���� ��������
            Destroy(effect, EfectStateInfo.length); //�ִϸ��̼� ���̸�ŭ ���� �� ����

            // ���� ���� ȿ���� ���
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

        if (isMoving)   // ���ڱ� �Ҹ� Ÿ�̹�
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

    IEnumerator AttackDelay(float AttackSpeed)  // �߰� ���� �ӵ�, ������ 0
    {
        var Delay = PlayerState.Instance.attackDelay;
        yield return new WaitForSeconds(Delay - AttackSpeed / 100f * Delay);
        canAttack = true;
    }
}