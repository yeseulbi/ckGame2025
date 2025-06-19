using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    [Header("���� ���� ����Ʈ ������")]
    public GameObject[] Weapon_Effect;

    public Animator Player_Anim, Weapon_Anim;
    public GameObject Attack_Effect, Weapon_HitBox;

    [Header("���� ����, �̵� �ӵ�")]
    public float force = 11;
    public float speed = 5f;
    Vector3 lookDirection = Vector2.right; // �ٶ󺸴� ������ Vector2�� ����

    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    bool onGround;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        // �¿� �̵� & �̵� �ִϸ��̼� ���� ������Ʈ
        if (horizontal != 0)
        {
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

            // Sprite ���� ����
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);

            lookDirection = new Vector3(Mathf.Sign(horizontal), 0); // �ٶ󺸴� ���� ������Ʈ

        }
        Player_Anim.SetFloat("moveStep", Mathf.Abs(horizontal));

        // ����
        if (onGround && Input.GetKeyDown(KeyCode.UpArrow))
            rb.AddForceY(force);
        else if (rb.linearVelocityY>0 && Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.linearVelocityY *= Input.GetAxis("Vertical");
        }        
        // ���� �ִϸ��̼� ���� ������Ʈ
        if(!onGround)
        {
            if (rb.linearVelocityY >0)
                Player_Anim.SetInteger("onSky", 1);
            else if (rb.linearVelocityY < 0)
                Player_Anim.SetInteger("onSky", -1);
        }
        else
        {
            Player_Anim.SetInteger("onSky", 0);
        }

        // ����
        if(Input.GetKeyDown(KeyCode.X))
        {
            // ���� HitBox Ȱ��ȭ
            Weapon_HitBox.SetActive(true);
            PlayerWeaponHitBox.Instance.entered = true; // �ڷ�ƾ Ȱ��ȭ
            Weapon_Anim.SetTrigger("Attack_1");
            var effect = Instantiate(Weapon_Effect[0], lookDirection, Quaternion.identity, transform);
            
            AnimatorStateInfo EfectStateInfo = effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);  //�ִϸ��̼� ���� ���� ��������
            Destroy(effect, EfectStateInfo.length); //�ִϸ��̼� ���̸�ŭ ���� �� ����

            /*if(effect==null)
                Weapon_HitBox.SetActive(false);*/
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
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
}
