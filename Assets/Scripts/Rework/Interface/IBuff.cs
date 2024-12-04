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
    float Ratio { get; }
    bool IsDebuff { get; }
}