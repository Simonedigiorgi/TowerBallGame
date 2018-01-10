using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {

    public PlayerController player;                                             // PLAYER
    public GameManager gameManager;                                             // GAMEMANAGER
    public CameraController cameraController;                                   // CAMERA CONTROLLER

    public enum PLATFORMS { Underworld, Move }                                  // Underworld = Permette di spostarsi nelle 2 dimensioni, Move = Trasporta il Player
    public PLATFORMS platforms;

    [Header("Underworld")]
    public float rotSpeed = 300f;                                               // Velocità di rotazione
    public float rotation = 0f;                                                 // Angolo della rotazione

    [Header("Move")]
    public Transform[] targets;                                                 // Arrays dei Targets
    public int speed;                                                           // Velocità di spostamento

    [Header("Debug")]
    private bool isOnPlatform;                                                  // Il giocatore si trova nel trigger della Piattaforma
    [HideInInspector] public Quaternion qto = Quaternion.identity;              // Quaternione

    void Update () {

        // MOVE

        if(platforms == PLATFORMS.Move)
        {
            if (cameraController.is180 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[0].position, speed * Time.deltaTime);
            }
            else if(cameraController.is0 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[1].position, speed * Time.deltaTime);
            }
        }

        // UNDERWORLD

        if (platforms == PLATFORMS.Underworld)
        {
            if (Input.GetKeyDown(KeyCode.S) && player.isUnderworld == false)
            {
                if (isOnPlatform)
                {
                    rotation = 180;
                    qto = Quaternion.Euler(rotation, 0, 0);
                    gameManager.changes++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) && player.isUnderworld == true)
            {
                if (isOnPlatform)
                {
                    rotation = 0;
                    qto = Quaternion.Euler(rotation, 0, 0);
                    gameManager.changes++;
                }
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, rotSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (platforms == PLATFORMS.Underworld)
        {
            if (other.gameObject.tag == "Player")
            {
                other.transform.parent = transform;
                player.rb.useGravity = false;
                isOnPlatform = true;
            }
        }

        if (platforms == PLATFORMS.Move)
        {
            if (other.gameObject.tag == "Player")
            {
                other.transform.parent = transform;
                isOnPlatform = true;
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {

        if (platforms == PLATFORMS.Underworld)
        {
            if (other.gameObject.tag == "Player")
            {
                other.transform.parent = null;
                player.rb.useGravity = true;
                isOnPlatform = false;

            }
        }

        if (platforms == PLATFORMS.Move)
        {
            if (other.gameObject.tag == "Player")
            {
                other.transform.parent = null;
                isOnPlatform = false;
            }
        }

    }
}
