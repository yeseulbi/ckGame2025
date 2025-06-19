using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerMove : MonoBehaviour
{
    [Header("무기 공격 이펙트 프리팹")]
    public GameObject[] Weapon_Effect;

    public Animator Player_Anim, Weapon_Anim;
    public GameObject Attack_Effect, Weapon_HitBox;

    [Header("점프 높이, 이동 속도")]
    public float force = 11;
    public float speed = 5f;
    Vector3 lookDirection = Vector2.right; // 바라보는 방향을 Vector2로 저장

    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    bool onGround;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");

        // 좌우 이동 & 이동 애니메이션 상태 업데이트
        if (horizontal != 0)
        {
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);

            // Sprite 방향 반전
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);

            lookDirection = new Vector3(Mathf.Sign(horizontal), 0); // 바라보는 방향 업데이트

        }
        Player_Anim.SetFloat("moveStep", Mathf.Abs(horizontal));

        // 점프
        if (onGround && Input.GetKeyDown(KeyCode.UpArrow))
            rb.AddForceY(force);
        else if (rb.linearVelocityY>0 && Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.linearVelocityY *= Input.GetAxis("Vertical");
        }        
        // 공중 애니메이션 상태 업데이트
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

        // 공격
        if(Input.GetKeyDown(KeyCode.X))
        {
            // 공격 HitBox 활성화
            Weapon_HitBox.SetActive(true);
            PlayerWeaponHitBox.Instance.entered = true; // 코루틴 활성화
            Weapon_Anim.SetTrigger("Attack_1");
            var effect = Instantiate(Weapon_Effect[0], lookDirection, Quaternion.identity, transform);
            
            AnimatorStateInfo EfectStateInfo = effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);  //애니메이션 상태 정보 가져오기
            Destroy(effect, EfectStateInfo.length); //애니메이션 길이만큼 지속 후 제거

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
