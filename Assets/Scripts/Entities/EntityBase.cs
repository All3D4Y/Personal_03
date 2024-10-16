using UnityEngine;

public interface ISetId
{
    public int ID { get; set; }
    public void SetID(EntityData entity);
}
