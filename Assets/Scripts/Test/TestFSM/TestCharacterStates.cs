using UnityEngine;

namespace TestNameSpace
{
    public class BattlePrep : TestState
    {
        public override void Enter(TestCharacter character)
        {
            character.PrintText("BattlePrep 상태 진입");
        }

        public override void Exit(TestCharacter character)
        {
            character.PrintText("BattlePrep 상태 종료");
        }

        public override void Update(TestCharacter character)
        {
            character.PrintText("BattlePrep 상태 진행 중");
        }
    }
    public class SelectAction : TestState
    {
        public override void Enter(TestCharacter character)
        {
            character.PrintText("SelectAction 상태 진입");
        }

        public override void Exit(TestCharacter character)
        {
            character.PrintText("SelectAction 상태 종료");
        }

        public override void Update(TestCharacter character)
        {
            character.PrintText("SelectAction 상태 진행 중");
        }
    }
    public class Battle : TestState
    {
        public override void Enter(TestCharacter character)
        {
            character.PrintText("Battle 상태 진입");
        }

        public override void Exit(TestCharacter character)
        {
            character.PrintText("Battle 상태 종료");
        }

        public override void Update(TestCharacter character)
        {
            character.PrintText("Battle 상태 진행 중");
        }
    }
}