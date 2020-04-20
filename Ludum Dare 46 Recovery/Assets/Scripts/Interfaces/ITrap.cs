using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrap 
{
    void TriggerTrap(Entity entity);
    void ResetTrap();
    void DisableTrap();
}
