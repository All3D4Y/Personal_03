using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattlePhase
{
    public enum PhaseEnums
    {
        BattleEnter = 0,
        BattlePrep,
        Action,
        OnBattle,
        BattleEnd
    }

    public class Phase : StateMachine
    {
        IState[] states;

        public IState Enter => states[0];
        public IState Prep => states[1];
        public IState Action => states[2];
        public IState Battle => states[3];
        public IState End => states[4];

        public Phase(BattleManager battleManager)
        {
            states = new IState[(int)PhaseEnums.BattleEnd + 1]; // 중간에 상태 추가할 때를 대비해서 (BattleEnd는 항상 마지막)

            this.states[0] = new BattleEnterState(battleManager);
            this.states[1] = new BattlePrepState(battleManager);
            this.states[2] = new BattleActionState(battleManager);
            this.states[3] = new BattleExecuteState(battleManager);
            this.states[4] = new BattleEndState(battleManager);
        }
        public override void Initialize(IState start)
        {
            
            base.Initialize(start);
        }

        public override void ChangeState(IState nextState)
        {
            base.ChangeState(nextState);
        }


        public override void Execute()
        {
            if (CurrentState != null)
            {
                CurrentState.Execute();
            }
        }
    }
}
