using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stick : Burnable
{
    public bool isActive;
    ParticleSystem burnParticle;
    Light burnLight;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 1;
        objType = BurnableType.Stick;
        burnParticle = GetComponentInChildren<ParticleSystem>();
        burnLight = GetComponentInChildren<Light>();
    }
    public override void UseObject(PlayerClass player)
    {
        if (isActive)
            return;
        else
        {
            player.TakeDamage(-BurnValue); // adds health
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
        Destroy(this.gameObject,1.5f);
    }
    public override void ResetObject()
    {
        Debug.LogWarning("Unintended Object Tried To Reset " + this.name);
    }
}
