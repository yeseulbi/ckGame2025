using UnityEngine;



public class WeaponSet : MonoBehaviour
{
    public static WeaponSet Instance { get; private set; }
    SpriteRenderer sr;

    [Header("���� ȿ����")]
    public AudioClip[] useWeapon_SF;

    [Header("Ÿ�� ȿ����")]
    public AudioClip[] Attack_SF;
    AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void WeaponSF_Play(int Index)
    {
        audioSource.clip = useWeapon_SF[Index];
        audioSource.Play();
    }

    public void AttackSF_Play(int Index)
    {
        audioSource.PlayOneShot(Attack_SF[Index]);
    }
    public void WeaponSpriteChange(Sprite set)
    {
        sr.sprite = set;
    }
}
