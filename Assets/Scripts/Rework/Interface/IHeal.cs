public enum HealType
{
    HP = 0,
    MP
}
public interface IHeal
{
    HealType Type { get; }
    float Amount { get; }
}
