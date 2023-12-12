using Keiwando.BigInteger;

// 모든 능력치 객체가 따라야 할 기본 인터페이스
public interface IStatus 
{
    BigInteger attack { get; set; }
    BigInteger health { get; set; }
    BigInteger defense { get; set; }
    float critChance { get; set; }
    float critDamage { get; set; }

}

// 모든 능력치 객체의 기본적인 행동 정의
public class BaseStatus : IStatus
{
    public BigInteger attack { get; set; } // 공격력
    public BigInteger health { get; set; } // 체력
    public BigInteger defense { get; set; } // 방어력

    // 치명타 확률과 치명타 데미지는 일반적으로 큰 수치가 아니므로, float 사용
    public float critChance { get; set; } // 크리티컬 확률
    public float critDamage { get; set; } // 크리티컬 데미지
}




public abstract class StatusDecorator : IStatus
{
    protected IStatus wrappedStats;

    protected StatusDecorator(IStatus status)
    {
        wrappedStats = status;
    }

    public virtual BigInteger attack
    {
        get => wrappedStats.attack;
        set => wrappedStats.attack = value;
    }
    public virtual BigInteger health
    {
        get => wrappedStats.health;
        set => wrappedStats.health = value;
    }
    public virtual BigInteger defense
    {
        get => wrappedStats.defense;
        set => wrappedStats.defense = value;
    }
    public virtual float critChance
    {
        get => wrappedStats.critChance;
        set => wrappedStats.critChance = value;
    }
    public virtual float critDamage
    {
        get => wrappedStats.critDamage;
        set => wrappedStats.critDamage = value;
    }
}