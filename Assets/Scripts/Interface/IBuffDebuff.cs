public interface IBuffDebuff
{
    BuffDebuffType BuffDebuffType { get; }

    float BuffRatio { get; }

    int Duration { get; }

    void BuffDebuff(BuffDebuffType type, BattleSlot[] targets);

    void Do();

    void Undo();
}