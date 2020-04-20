using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light lightSource;
    PlayerClass playerClass;
    [SerializeField]protected int Scalar = 1;
    private void Start()
    {
        playerClass = GetComponentInParent<PlayerClass>();
        lightSource = GetComponent<Light>();
    }

    private void Update()
    {
        lightSource.intensity = (float)playerClass.GetHealth();
    }

}