using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorStatusUI : MonoBehaviour
{
    Slider hp;
    Slider mp;

    Actor actor = null;

    public Actor Actor
    {
        get => actor;
        set => actor = value;
    }

    void Awake()
    {
        hp = transform.GetChild(0).GetComponent<Slider>();
        mp = transform.GetChild(1).GetComponent<Slider>();
    }

    void LateUpdate()
    {
        if (Camera.main.WorldToScreenPoint(actor.gameObject.transform.position) != transform.position)
        {
            transform.position = Camera.main.WorldToScreenPoint(actor.transform.position);
        }
    }

    public void OnHPChanged(float value)
    {
        hp.value = value;
    }

    public void OnMPChanged(float value)
    {
        mp.value = value;
    }
}
