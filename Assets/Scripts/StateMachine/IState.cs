using System.Collections;
using UnityEngine;

//Enter
//1. stageData 불러오기
//2. actor 배치하기
//
//Prep
//3. 턴 계산하고 턴 지정하기
//4. 턴 지정되면 액션으로 넘어가기, 한쪽이 전멸했으면 End state 로
//
//Action
//5. 스크롤인풋 battleManager.OnMoveSlot 에 등록
//6. 행동 선택하면 execute battle
//7. 스크롤인풋 battleManager.OnMoveSlot 에서 해제
//
//Execute
//8. 애니메이션 재생, 대미지 계산, 죽음 결정
//9. SortSlot, TurnCount ++
//
//Prep
//10. 전멸 여부에 따라 다음 상태 결정하기
//
//End
//11. 이겼는지 졌는지에 따라 결산화면으로

public interface IState
{
    public void Enter();

    public void Execute();

    public void Exit();
}