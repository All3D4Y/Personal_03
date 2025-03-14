using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public float moveSpeed; // 캐릭터 이동 속도

    /// <summary>
    /// 캐릭터의 슬롯 이동을 부드럽게 처리하는 함수
    /// </summary>
    /// <param name="character">움직일 캐릭터</param>
    /// <param name="toPos">목표 위치</param>
    public void OnMoveCharacter(Character character, Vector3 toPos)
    {
        StartCoroutine(OnMoveCharacterCoroutine(character, toPos));
    }

    /// <summary>
    /// 마법 이펙트를 시간차로 발생시키는 함수
    /// </summary>
    /// <param name="position">위치</param>
    /// <param name="isRight">오른쪽인지</param>
    public void OnMagicHitEffect(Vector3 position, bool isRight)
    {
        StartCoroutine(OnMagicHitEffectCoroutine(position, isRight));
    }

    /// <summary>
    /// 딜레이를 주고 상태를 변경하는 함수
    /// </summary>
    /// <param name="delay">딜레이 시간</param>
    /// <param name="stateType">목표 상태</param>
    public void OnChangeState(float delay, Type stateType)
    {
        StartCoroutine(OnChangeStateCoroutine(delay, stateType));
    }

    /// <summary>
    /// 캐릭터 슬롯 이동 시 부드럽게 움직이기 위한 코루틴
    /// </summary>
    /// <param name="character">움직일 캐릭터</param>
    /// <param name="toPos">목표 위치</param>
    /// <returns></returns>
    IEnumerator OnMoveCharacterCoroutine(Character character, Vector3 toPos)
    {
        OnTurnEffect onTurnEffect = GameManager.Instance.BattleManager.OnTurnEffect;

        if (character.transform.position.x < toPos.x)
        {
            while (character.transform.position.x < toPos.x)
            {
                character.transform.Translate(-moveSpeed * Time.deltaTime * Vector3.left);
                character.CUI.TransformUpdate();
                onTurnEffect.TransformUpdate();
                if (character.transform.position.x > toPos.x)
                    character.transform.position = toPos;
                yield return null;
            }
        }
        else if (character.transform.position.x > toPos.x)
        {
            while (character.transform.position.x > toPos.x)
            {
                character.transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
                character.CUI.TransformUpdate();
                onTurnEffect.TransformUpdate();
                if (character.transform.position.x < toPos.x)
                    character.transform.position = toPos;
                yield return null;
            }
        }
    }

    /// <summary>
    /// 마법 이펙트에 딜레이를 주기 위한 코루틴
    /// </summary>
    /// <param name="position">위치</param>
    /// <param name="isRight">오른쪽인지</param>
    /// <returns></returns>
    IEnumerator OnMagicHitEffectCoroutine(Vector3 position, bool isRight)
    {
        yield return new WaitForSeconds(1);
        Factory.Instance.GetMagicHitEffect(position, isRight);
    }

    /// <summary>
    /// 상태머신에 딜레이를 주기 위한 코루틴
    /// </summary>
    /// <param name="delay">딜레이 시간</param>
    /// <param name="stateType">목표 상태</param>
    /// <returns></returns>
    IEnumerator OnChangeStateCoroutine(float delay, Type stateType)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.BattleManager.ChangeState(stateType);
    }
}
