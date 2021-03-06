﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Burnable
{
    public bool isActive;
    ParticleSystem burnParticle;
    public bool playerIsWithinRange = false;
    PlayerClass player;
    private void Start() // Set burnable type assign burnable value
    {
        BurnValue = 3;
        objType = BurnableType.WoodCrate;
        burnParticle = GetComponentInChildren<ParticleSystem>();
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
            if (burnParticle.isPaused)
            {
                burnParticle.Play();
            }
        }
        else
        {
            if (burnParticle.isPlaying)
            {
                burnParticle.Pause();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entity") && other.gameObject.name == GameManager.playerName)
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
        Destroy(this.gameObject, 1.5f);
    }
    public override void ResetObject()
    {
        Debug.LogWarning("Unintended Object Tried To Reset " + this.name);
    }
}
