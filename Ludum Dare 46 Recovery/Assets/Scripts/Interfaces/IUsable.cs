using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable 
{
    void UseObject(PlayerClass player);
    void UseObject(PlayerClass player,ParticleSystem system, Vector3 pos);
    void DestroyObject();
    void ResetObject();
}
