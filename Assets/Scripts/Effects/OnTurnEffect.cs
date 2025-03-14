using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurnEffect : MonoBehaviour
{
    /// <summary>
    /// 활성화 시키기
    /// </summary>
    public void OnVisible()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 비활성화 시키기
    /// </summary>
    public void OnTransparent()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 차례인 캐릭터의 위치로 이동시키기
    /// </summary>
    public void TransformUpdate()
    {
        transform.position = GameManager.Instance.BattleManager.OnTurnCharacter.transform.position;
    }
}
