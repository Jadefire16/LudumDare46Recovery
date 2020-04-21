using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetScript : MonoBehaviour
{
    PlayerClass classofPlayer;

    private void Start()
    {
        classofPlayer = FindObjectOfType<PlayerClass>();
        classofPlayer.lastCheckPoint = this.transform;
        classofPlayer.transform.position = this.transform.position;
    }
}
