using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Roots : Burnable
{
    public bool isActive;
    BoxCollider col;
    ParticleSystem burnParticle;
    Light burnLight;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 2;
        objType = BurnableType.Root;
        burnParticle = GetComponentInChildren<ParticleSystem>();
        burnLight = GetComponentInChildren<Light>();
        col = GetComponent<BoxCollider>();
    }
    public override void UseObject(PlayerClass player)
    {
        if (isActive)
            return;
        else
        {
            player.TakeDamage(BurnValue); // reduces health
            isActive = true;
            DestroyObject();
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            burnLight.enabled = true;
            if (burnParticle.isPaused)
            {
                burnParticle.Play();
            }
        }
        else
        {
            burnLight.enabled = false;
            if (burnParticle.isPlaying)
            {
                burnParticle.Pause();
            }
        }
    }

    public override void UseObject(PlayerClass player, ParticleSystem system, Vector3 pos)
    {
        UseObject(player);
        Instantiate(system, pos, Quaternion.identity);
    }
    public override void DestroyObject()
    {
        col.enabled = false;
        Destroy(this.gameObject, 1.5f);
    }
    public override void ResetObject()
    {
        Debug.LogWarning("Unintended Object Tried To Reset " + this.name);
    }
}
