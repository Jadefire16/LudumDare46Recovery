using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerNextScene : MonoBehaviour
{
    SceneHandler handler;

    private void Awake()
    {
        handler = FindObjectOfType<SceneHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entity"))
        {
            handler.LoadNextScene();
        }
    }
}
