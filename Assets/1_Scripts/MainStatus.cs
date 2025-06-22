using System.Collections.Generic;
using System.Linq;

public enum StatusEffect
{
    Poison,         // 바이러스
    Frozen,         // 네트워크 방해
    Burn,           // 과열
    Invincible      // 무적
}
public enum Buff
{
    strUp,          // 공격력 증가
    recoveryUp,     // 회복 증가
    Vampirism,      // 흡혈
    PoisonRes,      // 바이러스 저항
    FrozenRes,      // 네트워크 방해 저항
    BurnRes         // 과열 저항
}
public enum Debuff
{
    strDown,         // 공격력 감소
    recoveryDown,    // 회복 감소
    defDown,         // 방어력 감소(받는 공격력 증가)
    PoisonWeak,      // 바이러스 약점
    FrozenWeak,      // 네트워크 방해 약점
    BurnWeak         // 과열 약점
}
public class MainStatus
{
    public Dictionary<StatusEffect, float> effectTimers = new Dictionary<StatusEffect, float>(3);
    public List<Buff> buffs = new List<Buff>();
    public List<Debuff> debuffs = new List<Debuff>();

    public void AddStatusEffect(StatusEffect effect, float AddTime)  // 상태 이상 부여 혹은 지속 시간 증가
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
    public void Sorting()   // 추가 또는 변경될 때 지속 시간이 높은 순서대로 정렬(아이콘 정렬에 사용)
    {
        var sort = effectTimers.OrderByDescending(num => num.Value);
    }

    public bool HasEffect(StatusEffect effect)      // 현재 상태이상 확인
    {
        return effectTimers.ContainsKey(effect) && effectTimers[effect] > 0;
    }

    public void RemoveEffect(StatusEffect effect)
    {
        if (effectTimers.ContainsKey(effect))
            effectTimers.Remove(effect);
    }
}