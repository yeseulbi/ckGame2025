using System.Collections.Generic;
using System.Linq;

public enum StatusEffect
{
    Poison,         // ���̷���
    Frozen,         // ��Ʈ��ũ ����
    Burn,           // ����
    Invincible      // ����
}
public enum Buff
{
    strUp,          // ���ݷ� ����
    recoveryUp,     // ȸ�� ����
    Vampirism,      // ����
    PoisonRes,      // ���̷��� ����
    FrozenRes,      // ��Ʈ��ũ ���� ����
    BurnRes         // ���� ����
}
public enum Debuff
{
    strDown,         // ���ݷ� ����
    recoveryDown,    // ȸ�� ����
    defDown,         // ���� ����(�޴� ���ݷ� ����)
    PoisonWeak,      // ���̷��� ����
    FrozenWeak,      // ��Ʈ��ũ ���� ����
    BurnWeak         // ���� ����
}
public class MainStatus
{
    public Dictionary<StatusEffect, float> effectTimers = new Dictionary<StatusEffect, float>(3);
    public List<Buff> buffs = new List<Buff>();
    public List<Debuff> debuffs = new List<Debuff>();

    public void AddStatusEffect(StatusEffect effect, float AddTime)  // ���� �̻� �ο� Ȥ�� ���� �ð� ����
    {
        if (effectTimers.Count<3)
        {
            if(effectTimers.ContainsKey(effect))
                effectTimers[effect] += AddTime;
            else
                effectTimers.Add(effect, AddTime);
        }
        Sorting();
    }
    public void Sorting()   // �߰� �Ǵ� ����� �� ���� �ð��� ���� ������� ����(������ ���Ŀ� ���)
    {
        var sort = effectTimers.OrderByDescending(num => num.Value);
    }

    public bool HasEffect(StatusEffect effect)      // ���� �����̻� Ȯ��
    {
        return effectTimers.ContainsKey(effect) && effectTimers[effect] > 0;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        if (effectTimers.ContainsKey(effect))
            effectTimers.Remove(effect);
    }
}