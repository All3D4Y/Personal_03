using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    static int validID = 0;

    int id;
    public int ID
    {
        get => id;
        set
        {
            id = value;
            validID++;
        }
    }

    string entityName;
    string entityColor;

    public virtual void Setup(string name)
    {
        ID = validID;
        entityName = name;
        int color = Random.Range(0, 255);
        entityColor = $"#{color.ToString("X6")}";
    }

    public abstract void Updated();

    public void PrintText(string text)
    {
        Debug.Log($"<color={entityColor}><b>{entityName}</b></color> : {text}");
    }
}
