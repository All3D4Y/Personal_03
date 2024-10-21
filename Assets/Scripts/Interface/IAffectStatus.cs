using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffectStatus
{
    public AffectType AffectType { get; set; }

    public void Affect(AffectType type);

}
