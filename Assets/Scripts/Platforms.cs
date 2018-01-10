using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour {

    public PlayerController player;                                             // PLAYER
    public GameManager gameManager;                                             // GAMEMANAGER
    public CameraController cameraController;                                   // CAMERA CONTROLLER

    /*private Color color1 = Color.red;
    private Color color2 = Color.blue;
    private float duration = 3.0f;*/

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
    private bool isActive;
    [HideInInspector] public Quaternion qto = Quaternion.identity;              // Quaternione

    private void Start()
    {
        isActive = true;
        cameraController.GetComponentInChildren<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }

    void Update () {

        // MOVE

        if(platforms == PLATFORMS.Move)
        {
            if (cameraController.isRotating180 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[0].position, speed * Time.deltaTime);
            }
            else if(cameraController.isRotating0 == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[1].position, speed * Time.deltaTime);
            }
        }

        // UNDERWORLD

        if (platforms == PLATFORMS.Underworld)
        {
            if (Input.GetKeyDown(KeyCode.S) && player.isUnderworld == false && isActive)
            {
                if (isOnPlatform)
                {
                    //ChangeColor();

                    rotation = 180;
                    qto = Quaternion.Euler(rotation, 0, 0);
                    gameManager.changes++;

                    StartCoroutine(TimeScale());
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) && player.isUnderworld == true && isActive)
            {
                if (isOnPlatform)
                {
                    rotation = 0;
                    qto = Quaternion.Euler(rotation, 0, 0);
                    gameManager.changes++;

                    StartCoroutine(TimeScale());
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

    public IEnumerator TimeScale()
    {
        isActive = false;
        player.isJump = false;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 1f;
        player.isJump = true;
        isActive = true;
    }

    /*public void ChangeColor()
    {
        var t = Mathf.PingPong(Time.time, duration) / duration;
        cameraController.GetComponentInChildren<Camera>().backgroundColor = Color.Lerp(color1, color2, t);
    }*/
}
