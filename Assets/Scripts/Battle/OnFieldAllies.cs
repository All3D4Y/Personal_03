using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OnFieldAllies Data", menuName = "Scripable Objects/OnFieldAllies Data", order = 1)]
public class OnFieldAllies : ScriptableObject
{
    public Ally[] allies;
}
