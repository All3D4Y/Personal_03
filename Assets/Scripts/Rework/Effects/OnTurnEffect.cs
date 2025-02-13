using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurnEffect : MonoBehaviour
{
    public void OnVisible()
    {
        this.gameObject.SetActive(true);
    }

    public void OnTransparent()
    {
        this.gameObject.SetActive(false);
    }

    public void TransformUpdate()
    {
        transform.position = GameManager.Instance.BattleManager.OnTurnCharacter.transform.position;
    }
}
