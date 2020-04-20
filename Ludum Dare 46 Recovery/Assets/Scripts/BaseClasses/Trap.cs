using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, ITrap
{
    protected int damage = 1;
    protected bool isActive;
    public virtual void DisableTrap() { isActive = false; } // Call after trigger trap to ensure player isnt hit multiple times

    public virtual void ResetTrap() { isActive = true; } // use this to reset trap (along with a coroutine delay or delay of yours choice)

    public virtual void TriggerTrap(Entity entity) { } // Used to trigger traps
}
