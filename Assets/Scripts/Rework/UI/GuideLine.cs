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

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        guides = new Image[4];

        for (int i = 0; i < guides.Length; i++)
        {
            guides[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }
    public void Initialize(int targetCount, int index)
    {
        foreach (var guide in guides)
        {
            guide.gameObject.SetActive(false);
        }

        if (targetCount > 0)
        {
            guides[targetCount - 1].gameObject.SetActive(true);

            RectTransform temp = guides[targetCount - 1].rectTransform;
            temp.localPosition = new Vector3(temp.localPosition.x, temp.localPosition.y + 25 * index, temp.localPosition.z);
            temp.sizeDelta = new Vector2(temp.sizeDelta.x, temp.sizeDelta.y + 50 * index);
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
}
