using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraFOV : MonoBehaviour {

    private CameraBackground cameraBackground;                                             // CAMERA CONTROLLER

    [BoxGroup("Dimensione del Gizmo")]
    public Vector3 size;                                                                   // The Gizmo size
    [BoxGroup("Comandi della Telecamera")]
    public float FOV, speed;                                                               // Field of View, Velocità della Camera

    void Start () {
        cameraBackground = FindObjectOfType<CameraBackground>();
    }	

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cameraBackground.TargetFOV = FOV;
            cameraBackground.SpeedFOV = speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
