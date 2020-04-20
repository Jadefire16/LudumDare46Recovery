using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UselessCameraScripToYeetUrDaddyWith : MonoBehaviour
{
    public Transform target;
    public Transform Obstruction;
    public Vector3 offset;
    Transform t;
    public float smoothing = 8f;
    public LayerMask collidableObjects;

    private void Start()
    {
        t = transform;
        t.position = (target.position + offset);
        Obstruction = target;
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(currentPos, desiredPos, smoothing * Time.fixedDeltaTime);
        t.position = smoothPos;
        this.gameObject.transform.LookAt(target);
        CheckForObstruction();
    }

    private void CheckForObstruction()
    {
        float distance = (target.position - t.position).sqrMagnitude + 0.5f;
        if (Physics.Raycast(t.position,target.position - t.position, out RaycastHit hit, distance, collidableObjects ,QueryTriggerInteraction.Ignore))
        {
            if(hit.collider.tag != "Player" && hit.collider.tag != "Entity")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
       
    }
}
        //t.position = (target.position + offset);
