public enum BuffType
{
    Attack = 0,
    Defense,
    Speed
}
public interface IBuff
{
    BuffType Type { get; }
    int Duration { get; }
    int ElapsedTurn { get; set; }
    float Ratio { get; }
    bool IsDebuff { get; }
}