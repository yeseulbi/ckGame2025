using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // level, hp, str, EP, exp, speed, AttackSpeed

    public static PlayerState Instance { get; private set; }

    [Header("�ɷ�ġ")]
    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float strength = 25f;          // ���ݷ�
    public float EP = 100f;               // ��ų ����Ʈ
    public float moveSpeed = 5f;          // �̵��ӵ� (������ ���)
    [Tooltip("���� ������ �ӵ�")] public float attackDelay = 0.5f;        // ���ݼӵ� (������ ���) -> ���ݵ����̼ӵ�

    [Tooltip("���� ����ġ")] public float currentExp = 0;
    [Tooltip("���� �������� ����ġ")] public float nextLevelExp = 100f;

    private void Awake()
    {
        Instance = this;
    }
    public void LevelUp()
    {
        if (level < 10)
        {
            level++;
            maxHp += 20f;
            currentHp = maxHp;
            EP += 10;
            strength += 5f;
            currentExp = 0;
            nextLevelExp *= 1.6f;
        }
        else
        {
            Debug.Log("�ִ� ���� ����");
        }

       /* MainStatus.StatusEffectAdd.Add(StatusEffect.Invincible, 10f);

        Debug.Log(MainStatus.StatusEffectAdd.ContainsKey((StatusEffect)1));*/
    }
}
