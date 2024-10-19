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

        //public Phase(PhaseManager phaseManager)
        //{
        //    this.battleEnterState = new BattleEnterState(phaseManager);
        //    this.prepState = new BattlePrepState(phaseManager);
        //    this.actionState = new ActionState(phaseManager);
        //    this.onBattleState = new OnBattleState(phaseManager);
        //    this.battleEndState = new BattleEndState(phaseManager);
        //}
        public override void Initialize(IState start)
        {
            states = new IState[(int)PhaseEnums.BattleEnd + 1]; // 중간에 상태 추가할 때를 대비해서 (BattleEnd는 항상 마지막)

            states[0] = new BattleEnterState();
            states[1] = new BattlePrepState();
            states[2] = new ActionState();
            states[3] = new OnBattleState();
            states[4] = new BattleEndState();

            base.Initialize(start);
        }

        public override void ChangeState(IState nextState)
        {
            base.ChangeState(nextState);
            Debug.Log("BattlePhase 상태전환");
        }


        public override void Updated()
        {
        }
    }
}
