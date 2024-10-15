using UnityEngine;

public enum CharaterState
{
    BattlePrep = 0,
    SelectAction,
    Battle
}

public class TestCharacter : EntityBase
{
    public enum AttackTypeEnum
    {
        Normal = 0,
        MachineWeakness,
        HumanWeakness
    }

    TestState[] states;
    TestState currentState;

    public int AttackDamage { get; set; }
    public int DifensivePower { get; set; }
    public AttackTypeEnum AttackType { get; set; }

    public int ActionSpeed { get; set; }
    public int UltimateGauge { get; set; }

    public override void Setup(string name)
    {
        base.Setup(name);

        gameObject.name = $"{ID:D2}_Character_{name}";

        states = new TestState[3];
        states[(int)CharaterState.BattlePrep] = new TestNameSpace.BattlePrep();
        states[(int)CharaterState.SelectAction] = new TestNameSpace.SelectAction();
        states[(int)CharaterState.Battle] = new TestNameSpace.Battle();

        currentState = states[(int)CharaterState.BattlePrep];

        AttackDamage = 0 ;
        DifensivePower = 0 ;
        AttackType = AttackTypeEnum.Normal ;
        ActionSpeed = 0 ;
        UltimateGauge = 0 ;
    }

    public override void Updated()
    {
        //PrintText("Wait...");

        if ( currentState != null)
        {
            currentState.Update(this);
        }
    }

    public void ChangeState(CharaterState nextState)
    {
        if (states[(int)nextState] == null) return;

        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = states[(int)nextState];
        currentState.Enter(this);
    }
}
