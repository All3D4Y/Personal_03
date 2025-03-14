/// <summary>
/// 버프의 종류를 확인하는 열거형
/// </summary>
public enum BuffType
{
    Attack = 0,
    Defense,
    Speed
}

/// <summary>
/// 스킬이나 아이템이 버프의 기능을 할 때 사용할 인터페이스
/// </summary>
public interface IBuff
{
    /// <summary>
    /// 버프의 종류
    /// </summary>
    BuffType Type { get; }

    /// <summary>
    /// 버프 지속 기간(턴)
    /// </summary>
    int Duration { get; }

    /// <summary>
    /// 적용 후 경과된 턴
    /// </summary>
    int ElapsedTurn { get; set; }

    /// <summary>
    /// 버프로 능력치를 올려줄 배율
    /// </summary>
    float Ratio { get; }

    /// <summary>
    /// 디버프인지
    /// </summary>
    bool IsDebuff { get; }
}