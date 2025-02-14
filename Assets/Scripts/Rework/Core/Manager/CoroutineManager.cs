using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public float moveSpeed;

    public void OnMoveCharacter(Character character, Vector3 toPos)
    {
        StartCoroutine(OnMoveCharacterCoroutine(character, toPos));
    }

    public void OnMagicHitEffect(Vector3 position, bool isRight)
    {
        StartCoroutine(OnMagicHitEffectCoroutine(position, isRight));
    }

    public void OnChangeState(float delay, Type stateType)
    {
        StartCoroutine(OnChangeStateCoroutine(delay, stateType));
    }

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

    IEnumerator OnMagicHitEffectCoroutine(Vector3 position, bool isRight)
    {
        yield return new WaitForSeconds(1);
        Factory.Instance.GetMagicHitEffect(position, isRight);
    }

    IEnumerator OnChangeStateCoroutine(float delay, Type stateType)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.BattleManager.ChangeState(stateType);
    }
}
