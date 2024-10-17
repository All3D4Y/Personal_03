using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSlot
{
    EntityData entityData = null;

    public EntityType Type {  get; private set; }
    public uint Index { get;  private set; }

    public bool IsEmpty => entityData == null;



    public EntityData EntityData
    {
        get => entityData;
        private set
        {
            if (entityData != value)
                entityData = value;
        }
    }

    public BattleSlot(EntityType type, uint index)
    {
        Type = type;
        Index = index;
        EntityData = null;
    }

    public void AssignSlot(EntityData entity, bool isEmpty = true)
    {
        if (entity != null)
        {
            EntityData = entity;
            isEmpty = false;
        }
        else
        {
            EntityData = null;
            isEmpty = true;
        } 
    }
}
