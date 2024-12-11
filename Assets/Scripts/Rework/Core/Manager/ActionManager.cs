using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public ItemSkill SelectedAction { get; private set; }

    public void SetAction(ItemSkill action)
    {
        SelectedAction = action;
    }

    public void ActionExecute()
    {
        SelectedAction.Execute();
    }

    public void Clear()
    {
        SelectedAction = null;
    }
}
