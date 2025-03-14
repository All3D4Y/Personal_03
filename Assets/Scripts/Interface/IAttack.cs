/// <summary>
/// 스킬이나 아이템이 공격용이면 사용할 인터페이스
/// </summary>
public interface IAttack
{
    /// <summary>
    /// 공격력에 곱해 대미지를 산출하기 위한 계수
    /// </summary>
    float Ratio { get; }

    /// <summary>
    /// 크리티컬 발생 확률
    /// </summary>
    float CriticalRate { get; }

    /// <summary>
    /// 크리티컬 발생 시 추가 대미지
    /// </summary>
    float CriticalBonus { get; }

    /// <summary>
    /// 대미지 계산 공식
    /// </summary>
    /// <param name="user">스킬이나 아이템의 사용자</param>
    /// <param name="isCritical">크리티컬 발생 여부</param>
    /// <returns></returns>
    float DoDamage(Character user, out bool isCritical);
}