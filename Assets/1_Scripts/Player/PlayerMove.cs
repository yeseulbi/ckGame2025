using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    public float force;
    Animator animator;
    SpriteRenderer sr;
    float speed = 5f;
    Vector2 lookDirection = Vector2.right; // �ٶ󺸴� ������ Vector2�� ����

    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    bool onGround;

    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetBool("isRunning", true);
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

            // �Է� �������� lookDirection ����
            lookDirection = new Vector2(horizontal, 0).normalized;

            // Sprite ���� ����
            sr.flipX = lookDirection.x < 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (onGround&&Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.linearVelocityY = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
            onGround = true;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!onGround&&other.gameObject.CompareTag("Floor"))
            onGround = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        onGround = false;
    }
    void Jump()
    {
        rb.AddForceY(force);
        Debug.Log("��ȣ");
    }
}
