using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponHitBox : MonoBehaviour
{
    [Header("타격 이펙트 프리팹")]
    public GameObject[] Attack_Effect;
    
    public static PlayerWeaponHitBox Instance { get; private set; }
    [HideInInspector]public bool entered;
    AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
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
        if (collision.gameObject.CompareTag("Enemy")&&entered)  // OnTriggerEnter이 감지되지 않는 공중에서 HitBox가 열리면 닫히지 않는 문제 해결
        {
            Instantiate(Attack_Effect[0], transform.position, Quaternion.identity); // 타격 이펙트 배열 확장 변경
            WeaponSet.Instance.AttackSF_Play(0);
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