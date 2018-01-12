using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Platforms : MonoBehaviour {

    private CameraController cameraController;                                                      // CAMERA CONTROLLER
    private PlayerController player;                                                                // PLAYER
    private GameManager gameManager;                                                                // GAMEMANAGER
    private AudioSource source;

    public enum PLATFORMS { None, Underworld, Move, Rendering }
    [FoldoutGroup("SelectPlatform")]
    [HideLabel]
    [InfoBox("Seleziona il comportamento della piattaforma")]
    public PLATFORMS platforms;

    [InfoBox("Move ti permette di spostare il giocatore attraverso un Array di due Targets")]
    [TabGroup("Move")]
    public int speed;                                                                               // Velocità di spostamento

    [TabGroup("Move")]
    public Transform[] targets;                                                                     // Arrays dei Targets

    [InfoBox("Velocità di rotazione e angolo di rotazione attuale")]
    [TabGroup("Underworld")]
    public float rotSpeed = 300f;                                                                   // Velocità di rotazione

    [TabGroup("Underworld")]
    public float rotation = 0f;                                                                     // Angolo della rotazione
                                                                                                    
    private bool isOnPlatform;                                                                      // Il giocatore si trova nel trigger della Piattaforma
    private bool isActive;                                                                          // La piattaforma è attiva
    [HideInInspector] public Quaternion qto = Quaternion.identity;                                  // Quaternione

    [TabGroup("Rendering")]
    [InfoBox("Scegli da quale angolazione la piattaforma sarà invisibile")]
    public bool Rotation0;
    [TabGroup("Rendering")]
    public bool Rotation180, Rotation90, RotationLess90;
    [InfoBox("Disattiva le collisioni")]
    [TabGroup("Rendering")]
    public bool notWalkable;

    [TabGroup("Rendering")]
    public GameObject triggerUp, triggerDown; 

    [FoldoutGroup("Audio")]
    public AudioClip bass;

    private void Start()
    {
        isActive = true;

        cameraController = FindObjectOfType<CameraController>();
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        source = GetComponent<AudioSource>();


    }

    void Update () {

        // NONE

        if (platforms == PLATFORMS.None)
        {
            
        }

        // MOVE

        if (platforms == PLATFORMS.Move)
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

        // RENDERING

        if (platforms == PLATFORMS.Rendering)
        {
            if(cameraController.rotation == 0)
            {
                if(Rotation0 == true)
                {
                    GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    GetComponent<Renderer>().enabled = true;
                }
            }

            if (cameraController.rotation == 180)
            {
                if (Rotation180 == true)
                {
                    GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    GetComponent<Renderer>().enabled = true;
                }
            }

            if (cameraController.rotation == 90)
            {
                if (Rotation90 == true)
                {
                    GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    GetComponent<Renderer>().enabled = true;
                }
            }

            if (cameraController.rotation == -90)
            {
                if (RotationLess90 == true)
                {
                    GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    GetComponent<Renderer>().enabled = true;
                }
            }

            if(notWalkable == true)
            {
                GetComponent<BoxCollider>().enabled = false;

                triggerUp.SetActive(false);
                triggerDown.SetActive(false);
            }
            else
            {
                GetComponent<BoxCollider>().enabled = true;

                triggerUp.SetActive(true);
                triggerDown.SetActive(true);
            }

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
        source.PlayOneShot(bass, 0.4f);
        isActive = false;
        player.isJump = false;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 1f;
        player.isJump = true;
        isActive = true;
    }
}
