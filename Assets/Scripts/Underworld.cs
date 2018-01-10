﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underworld : MonoBehaviour {

    public PlayerController player;                                                         // PLAYER
    public Vector3 size;                                                                    // The Gizmo size

    [HideInInspector] public bool isUp;                                                     // Sopra
    [HideInInspector] public bool isDown;                                                   // Sotto

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isUp)
        {
            player.isUnderworld = false;
        }

        if (other.gameObject.tag == "Player" && isDown)
        {
            player.isUnderworld = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
