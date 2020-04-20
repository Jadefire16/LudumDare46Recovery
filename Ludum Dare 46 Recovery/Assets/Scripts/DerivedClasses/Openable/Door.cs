using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Door : Openable
{
    protected event Action OnObjOpenEvent;

    Collider col;
    float delayTime = 0.75f;
    float currTime;
    bool beginTimer = false;
    protected void Start()
    {
        col = GetComponent<BoxCollider>(); // maybe make Get component in children
        OnObjOpenEvent += OnOpen;
    }

    private void LateUpdate()
    {
        if(isOpen && beginTimer)
        {
            currTime += Time.deltaTime;
        }

        if (currTime >= delayTime)
        {
            OnClose();
        }
    }

    public override void OnClose() 
    {
        base.OnClose();
        currTime = 0f;
        col.enabled = true;
        beginTimer = false;
        // Play Animation
    }
    public override void OnOpen() 
    { 
        base.OnOpen();
        col.enabled = false;
        //Play animation
    }

    private void OnTriggerEnter(Collider other)
    {
        OnObjOpenEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
            beginTimer = true;
    }
}
