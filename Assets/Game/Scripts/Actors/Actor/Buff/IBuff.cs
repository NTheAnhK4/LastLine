using UnityEngine;

public interface IBuff
{
   //Used to get the required component from the target
    void Apply(GameObject target);
    void Update();
    void Remove();
}