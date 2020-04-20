using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Puddle : Trap
{
    public override void DisableTrap() => isActive = false;
    public override void ResetTrap() => isActive = true;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) // Detects when an entity has entered MUST HAVE ENTITY TAG
    {
        if(other.gameObject.CompareTag("Entity"))
        {
            TriggerTrap(other.GetComponent<Entity>());
        }
    }
    private void OnTriggerExit(Collider other) // detects when an entity has left MUST HAVE ENTITY TAG
    {
        if (other.gameObject.CompareTag("Entity"))
        {
            StartCoroutine(ResetAfterTime());
        }
    }
    public override void TriggerTrap(Entity entity) // used when the trap should be activated I.E. OnTiggerEnter
    {
        entity.TakeDamage(damage);
        DisableTrap();
    }

    IEnumerator ResetAfterTime() // waits half a second and then resets the trap
    {
        yield return new WaitForSeconds(0.5f);
        ResetTrap();
    }
}
