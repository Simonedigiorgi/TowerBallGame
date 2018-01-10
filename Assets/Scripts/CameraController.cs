using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController player;                                         // PLAYER
    public GameManager gameManager;                                         // GAMEMANAGER
    public Rotation arrow;                                                  // Richiama lo script Rotation della freccia

    public float speed = 300f;                                              // Velocità di rotazione
    public float rotation = 0f;                                             // Angolo della rotazione
    public float underRotation = 0f;                                        // Angolo di rotazione del sottosopra
    public bool isLateralButtons;                                           // Le frecce laterali sono attive 
    [HideInInspector] public Quaternion qto = Quaternion.identity;          // Quaternione della Camera

    [Header("Audio")]
    private AudioSource source;
    public AudioClip rotateSound;                                           // Suono della rotazione della stanza

    [Header("Debug")]
    [HideInInspector] public bool isRotating0;                              // La sua rotazione è (0)
    [HideInInspector] public bool isRotating180;                            // La sua rotazione è (180)
    [HideInInspector] public bool isActive;                                 // La camera è attiva

    void Start () {

        isLateralButtons = true;                                            // Le frecce laterali sono attive
        source = GetComponent<AudioSource>();

        arrow.qto = Quaternion.Euler(45, 0, 0);                             // Inizializza il quaternione della freccia
    }
	
	void Update () {     
        
        // SOPRAMONDO E SOTTOMONDO

        if(player.isUnderworld == false)
        {
            underRotation = 0;
            qto = Quaternion.Euler(0, rotation, underRotation);
        }

        if (player.isUnderworld == true)
        {
            underRotation = 180;
            qto = Quaternion.Euler(0, rotation, underRotation);
        }

        // ROTAZIONE DELLA CAMERA

        if (Input.GetKeyDown(KeyCode.UpArrow) && isActive == true && rotation != 0)
        {
            source.PlayOneShot(rotateSound);
            gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
            StartCoroutine(Rotate0());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isActive == true && rotation != 180)
        {
            source.PlayOneShot(rotateSound);
            gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
            StartCoroutine(Rotate180());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && isActive == true && rotation != 90 && isLateralButtons == true)
        {
            source.PlayOneShot(rotateSound);
            gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
            StartCoroutine(Rotate90());

        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && isActive == true && rotation != -90 && isLateralButtons == true)
        {
            source.PlayOneShot(rotateSound);
            gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
            StartCoroutine(RotateLess90());
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, speed * Time.deltaTime);
        arrow.transform.rotation = Quaternion.RotateTowards(arrow.transform.rotation, arrow.qto, arrow.rotSpeed * Time.deltaTime);

    }

    public IEnumerator RotateLess90()
    {

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        player.isActive = false;
        player.qto = Quaternion.Euler(0, -90, 0);
        arrow.qto = Quaternion.Euler(45, 0, -90);
        isActive = false;

        rotation = -90;
        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }

    public IEnumerator Rotate90()
    {

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.isActive = false;
        player.qto = Quaternion.Euler(0, 90, 0);

        arrow.qto = Quaternion.Euler(45, 0, 90);

        isActive = false;
        rotation = 90;
        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }

    public IEnumerator Rotate0()
    {

        isRotating180 = true;
        isRotating0 = false;

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.isActive = false;
        player.qto = Quaternion.Euler(0, 0, 0);

        arrow.qto = Quaternion.Euler(45, 0, 0);

        isActive = false;


        rotation = 0;

        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }

    public IEnumerator Rotate180()
    {
        isRotating0 = true;
        isRotating180 = false;

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.isActive = false;
        player.qto = Quaternion.Euler(0, rotation, 180);

        arrow.qto = Quaternion.Euler(45, 0, 180);

        isActive = false;

        rotation = 180;
        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }
}
