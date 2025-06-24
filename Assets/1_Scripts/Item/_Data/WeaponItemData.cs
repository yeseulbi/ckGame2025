using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponItemData", menuName = "Game/WeaponItemData")]
public class WeaponItemData : ItemData
{
    [Header("���� ���� ������")]
    public WeaponType weaponType;
    public float baseDamage;
    public float baseAttackDelay;
    public Vector2 attackPowerIncrease;
    public Vector2 uniqueStatIncrease;
    public Vector2 statusEffectChance;

    [HideInInspector] public StatusEffect statusEffect;  // ���� �����̻� - ���� ��� �̻� ���� �߰�
    [HideInInspector] public float statusEffectPercent; //  �����̻� �ߵ�Ȯ��
}