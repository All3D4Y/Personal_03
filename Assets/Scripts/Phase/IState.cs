using System.Collections;
using UnityEngine;

namespace BattlePhase
{
    public interface IState
    {
        public void Enter()
        {
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }

        protected virtual IEnumerator Execute()
        {
            Enter();        // 상태 진입 시 1회 호출
            while (true)
            {
                // Monobehaviour의 Update 대신 사용, 매프레임 호출
                Update();
                yield return null;
            }
        }
    }
}
