using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideLine : MonoBehaviour
{
    Image[] guides;

    RectTransform rect;

    Color invalidColor = new Color(1f, 1f, 1f, 0.3f);   // 스킬 사용이 유효하지 않을 경우 변경할 색상

    Vector2 defaultPos = new Vector2(0, 75);
    Vector2 defaultSize = new Vector2(500, 100);

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        guides = new Image[4];

        for (int i = 0; i < guides.Length; i++)
        {
            guides[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="targetCount">효과 적용 대상의 수</param>
    /// <param name="skillIndex">사용할 스킬의 인덱스</param>
    public void Initialize(int targetCount, int skillIndex)
    {
        ResetGuide();

        if (targetCount > 0)
        {
            guides[targetCount - 1].enabled = true;

            RectTransform temp = guides[targetCount - 1].rectTransform;
            temp.localPosition = new Vector3(temp.localPosition.x, temp.localPosition.y + 25 * (skillIndex - 1), temp.localPosition.z);
            temp.sizeDelta = new Vector2(temp.sizeDelta.x, temp.sizeDelta.y + 50 * skillIndex);
        }
    }

    /// <summary>
    /// 보조선의 위치 갱신용 함수
    /// </summary>
    /// <param name="range">스킬의 유효 거리</param>
    /// <param name="count">효과 적용 대상의 수</param>
    public void TransformUpdate(int range, int count)
    {
        float rectX = Mathf.Clamp(755 + (range - GameManager.Instance.BattleManager.OnTurnCharacter.Index - 5 + count) * 180, 215, 755);

        Vector3 pos = new Vector3(rectX, rect.localPosition.y, 0);

        rect.localPosition = pos;
    }

    /// <summary>
    /// 스킬 사용 유효 여부에 따라 색상을 변경하는 함수
    /// </summary>
    /// <param name="isValid">유효한지</param>
    public void ValidColor(bool isValid)
    {
        if (!isValid)
        {
            foreach (var guide in guides)
            {
                guide.color = invalidColor;
            }
        }
        else
        {
            foreach (var guide in guides)
            {
                guide.color = Color.white;
            }
        }
    }

    /// <summary>
    /// 보조선을 기본값으로 되돌리는 함수
    /// </summary>
    public void ResetGuide()
    {
        foreach (Image guide in guides)
        {
            guide.enabled = false;
            guide.rectTransform.localPosition = defaultPos;
            guide.rectTransform.sizeDelta = defaultSize;
        }
    }
}
