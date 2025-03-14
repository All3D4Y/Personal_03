using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    /// <summary>
    /// 실행할 행동 저장용 프로퍼티
    /// </summary>
    public Skill SelectedAction { get; private set; }

    /// <summary>
    /// 행동 설정
    /// </summary>
    /// <param name="action"></param>
    public void SetAction(Skill action)
    {
        SelectedAction = action;
    }

    /// <summary>
    /// 행동 실행
    /// </summary>
    public void ActionExecute()
    {
        SelectedAction.Execute();
    }

    /// <summary>
    /// 행동 삭제
    /// </summary>
    public void Clear()
    {
        SelectedAction = null;
    }
}
