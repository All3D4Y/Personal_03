/// <summary>
/// 회복의 종류가 체력인지 마인드인지 확인하는 열거형
/// </summary>
public enum HealType
{
    HP = 0,
    MP
}

/// <summary>
/// 스킬이나 아이템이 회복 기능을 할 때 사용 할 인터페이스
/// </summary>
public interface IHeal
{
    /// <summary>
    /// 회복의 종류
    /// </summary>
    HealType Type { get; }

    /// <summary>
    /// 회복양
    /// </summary>
    float Amount { get; }
}
