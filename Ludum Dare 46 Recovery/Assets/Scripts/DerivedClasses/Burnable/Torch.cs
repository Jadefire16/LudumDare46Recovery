using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Burnable
{
    public bool isActive;
    ParticleSystem torchParticle;
    Light torchLight;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 1;
        objType = BurnableType.Torch;
        torchParticle = GetComponentInChildren<ParticleSystem>();
        torchLight = GetComponentInChildren<Light>();
        isActive = Utils.AAUtilities.GetRandNumBool(0, 101);
    }
    public override void UseObject(PlayerClass player)
    {
        if (isActive)
        {
            player.TakeDamage(-BurnValue);
        }
        else
        {
            player.TakeDamage(BurnValue);
        }
        isActive = !isActive;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            torchLight.enabled = true;
            if (torchParticle.isPaused)
            {
                torchParticle.Play();
            }
        }
        else
        {
            torchLight.enabled = false;
            if (torchParticle.isPlaying)
            {
                torchParticle.Pause();
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
        Debug.LogWarning("Unintended Object Destroyed" + this.name);
    }
    public override void ResetObject()
    {
        Debug.LogWarning("Unintended Object Used" + this.name);
    }
}
