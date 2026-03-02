using System;

public class UnitHealth
{
    public event Action<float, float, float> OnHealthChanged;
    public event Action OnDeath;

    public float CurrentHp { get; private set; }
    public float MaxHp { get; }

    public UnitHealth(float maxHp)
    {
        MaxHp = maxHp;
        CurrentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentHp <= 0)
            return;

        CurrentHp = Math.Max(0, CurrentHp - damage);
        
        OnHealthChanged?.Invoke(MaxHp, CurrentHp, damage);

        if (CurrentHp <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}