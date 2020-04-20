using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour, IUsable
{
    protected BurnableType objType;
    private int burnValue = 1;

    protected int BurnValue { get => burnValue; set => burnValue = value; }

    public virtual void DestroyObject() { }
    public virtual void ResetObject() { }
    public virtual void UseObject(PlayerClass player) { }
    public virtual void UseObject(PlayerClass player, ParticleSystem system, Vector3 pos) { }

}

public enum BurnableType
{
    Torch,
    Campfire,
    WoodCrate,
    Stick,
    Root
}