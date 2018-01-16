using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Lerp : MonoBehaviour {

    private PlayerController player;                                                        // PLAYER CONTROLLER

    [FoldoutGroup("Lerp Colors")] public Color worldMaterial, underworldMaterial;
    [FoldoutGroup("Lerp Colors")] public float lerpDuration;
    [FoldoutGroup("Lerp Colors")] public bool isLerpActive;

    // LISTA COLORI

    /* Prato Verde = 115, 171, 78, 255
     * Prato Grigio = 119, 119, 119, 255
     * 
     * Muro di Terra = 114, 86, 67, 255
     * Muro Grigio = 165, 165, 165, 255
     * 
     *    
     * 
     * 
     */

    void Start () {

        player = FindObjectOfType<PlayerController>();

        if (GameObject.FindGameObjectWithTag("Ground"))
        {

        }
	}
	

	void Update () {

        if (isLerpActive == true)
        {
            if (player.isUnderworld == true)
            {
                transform.GetComponent<Renderer>().material.DOColor(underworldMaterial, lerpDuration);
            }
            else if (player.isUnderworld == false)
            {
                transform.GetComponent<Renderer>().material.DOColor(worldMaterial, lerpDuration);
            }
        }

    }
}
