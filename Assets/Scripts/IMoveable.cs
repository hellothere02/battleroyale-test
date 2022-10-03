using UnityEngine;

public interface IMoveable
{
    public int Speed { get; set; }
    public void Move(GameObject target);
}
