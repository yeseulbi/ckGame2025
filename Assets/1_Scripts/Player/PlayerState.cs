using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // level, hp, str, EP, exp, speed, AttackSpeed

    public int level = 1;
    public float maxHp = 100f;
    public float currentHp;
    public float strength = 25f;          // ���ݷ�
    public float EP = 100f;               // ��ų ����Ʈ
    public float moveSpeed = 5f;          // �̵��ӵ� (������ ���)
    public float attackSpeed = 3f;        // ���ݼӵ� (������ ���)

    public float currentExp = 0;
    public float nextLevelExp = 100f;

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
    }
}
