public interface IAction
{
    /// <summary>
    /// 효과를 주는 슬롯의 개수
    /// </summary>
    uint EffectCount { get;}

    /// <summary>
    /// 효과를 주는 거리
    /// </summary>
    uint EffectRange { get;}


    public void ActionExecute(BattleSlot user, BattleSlot[] targets);

    /// <summary>
    /// 스킬이나 아이템의 효과를 받을 타겟의 첫번 째 인덱스와 타겟 개수를 리턴하는 함수
    /// </summary>
    /// <param name="user">스킬이나 아이템의 사용자</param>
    /// <returns>item1: 타겟의 첫번 째 인덱스, item2: 타겟의 개수</returns>
    public BattleSlot[] SetTarget(BattleSlot user);
}