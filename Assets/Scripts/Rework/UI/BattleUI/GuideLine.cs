using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GuideLine : MonoBehaviour
{
    Image[] guides;

    RectTransform rect;

    Color invalidColor = new Color(1f, 1f, 1f, 0.3f);

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
    public void TransformUpdate(int range, int count)
    {
        float rectX = Mathf.Clamp(755 + (range - GameManager.Instance.BattleManager.OnTurnCharacter.Index - 5 + count) * 180, 215, 755);

        Vector3 pos = new Vector3(rectX, rect.localPosition.y, 0);

        rect.localPosition = pos;
    }

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
