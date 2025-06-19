using Unity.VisualScripting;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    public static WeaponSet Instance { get; private set; }

    [Header("���� ȿ����")]
    public AudioClip[] useWeapon_SF;

    [Header("Ÿ�� ȿ����")]
    public AudioClip[] Attack_SF;
    AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
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
}
