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
}
