using System.Collections;
using UnityEngine;

public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("타격 이펙트 프리팹")]
    public GameObject[] Attack_Effect;
    
    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (entered)
        {
            StartCoroutine(CheckTriggerTimeout()); // 코루틴 시작
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&entered)
        {
            Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // 타격 이펙트 배열 확장 변경
        }
        gameObject.SetActive(false); // HitBox 비활성화
    }
    /*private void Update()
    {
        if (transform.childCount == 0)
    }*/
    //데미지 추가

    IEnumerator CheckTriggerTimeout()
    {
        yield return new WaitForSeconds(0.1f);
        entered = false; // 0.1초 후에 entered를 false로 설정
        gameObject.SetActive(false); // HitBox 비활성화
    }
}