using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RedEnemy : Enemy
{

    private void Awake() {
        SetRb(GetComponent<Rigidbody>());
    }

    private void Start() {
        base.SetDetection();
        base.SetRange(5);
        base.SetSpeed(1);
        base.SetRotSpeed(5f);
        base.SetDamage(1);
        base.SetRun(3);
        base.SetAngle(90);
        base.SetRays(20);

    }

    private void Update() {
        base.Move();
    }



}
