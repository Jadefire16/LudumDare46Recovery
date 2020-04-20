using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Burnable
{
    PlayerClass player;
    public Transform respawnPoint;
    public MeshRenderer fireRend;
    public bool isActive = false;
    ParticleSystem fireParticle;
    Light fireLight;
    public bool playerIsWithinRange = false;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 1;
        objType = BurnableType.Campfire;
        fireParticle = GetComponentInChildren<ParticleSystem>();
        fireLight = GetComponentInChildren<Light>();
        if (!fireRend)
            Debug.LogWarning("You need to assign the fire material in campfire");
    }
    public override void UseObject(PlayerClass player)
    {
        if (isActive)
        {
            UseCampfire();
            return;
        }
        player.TakeDamage(BurnValue);
        isActive = true;
        AudioManager.instance.Play("CampFire_Loop", true);
    }

    private void UseCampfire()
    {
        EventManager.instance.InvokeSaveGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsWithinRange)
            UseObject(player);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            fireLight.enabled = true;
            fireRend.enabled = true;
            fireRend.sharedMaterial.EnableKeyword("_EMISSION");
            if (fireParticle.isStopped)
            {
                fireParticle.Play();
            }
        }
        else
        {
            fireLight.enabled = false;
            fireRend.material.DisableKeyword("_EMISSION");
            fireRend.enabled = false;
            if (fireParticle.isPlaying)
            {
                fireParticle.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Entity") && other.gameObject.name == GameManager.playerName)
        {
            playerIsWithinRange = true;
            player = other.gameObject.GetComponent<PlayerClass>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Entity") && other.gameObject.name == GameManager.playerName)
            playerIsWithinRange = false;
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
