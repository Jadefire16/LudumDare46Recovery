using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(WindZone))]
[RequireComponent(typeof(SphereCollider))]
public class WindTrap : Trap
{
    SphereCollider col;
    WindZone zone;
    float timeInZone;
    float timeToDealDamage = 1.5f;
    bool isWithinZone;
    PlayerClass player;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        zone = GetComponent<WindZone>();
        player = FindObjectOfType<PlayerClass>();
        zone.mode = WindZoneMode.Spherical;
        if (col.radius < 1)
            col.radius = 1;
        col.isTrigger = true;
    }

    private void Update()
    {
        if (isWithinZone)
        {
            TimeInZone += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isWithinZone = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimeInZone = 0;
            isWithinZone = false;
        }
    }
    public float TimeInZone
    {
        get => timeInZone;
        set
        {
            timeInZone = value;
            if (timeInZone > timeToDealDamage)
                player.TakeDamage(damage);
        }
    }


}
