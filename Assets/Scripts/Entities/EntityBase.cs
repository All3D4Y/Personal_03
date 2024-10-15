using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
    static int m_ValidID = 0;

    int id;
    public int ID
    {
        get => id;
        set
        {
            id = value;
            m_ValidID++;
        }
    }

    string entityName;
}
