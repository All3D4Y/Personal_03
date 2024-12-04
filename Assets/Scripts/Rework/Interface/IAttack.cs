public interface IAttack
{
    float Ratio { get; }
    float CriticalRate { get; }
    float CriticalBonus { get; }

    float DoDamage(float atk);
}