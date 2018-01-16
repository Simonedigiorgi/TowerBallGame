using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CameraController : MonoBehaviour {

    private PlayerController player;                                         // PLAYER
    private GameManager gameManager;                                         // GAMEMANAGER
    [HideInInspector] public Rotation arrow;                                 // Richiama lo script Rotation della freccia
    [HideInInspector] public Quaternion qto = Quaternion.identity;           // Quaternione della Camera
    private AudioSource source;

    [InfoBox("Velocità di rotazione e angolo di rotazione attuale")]
    [TabGroup("Quaternion")]
    public float speed = 300f;                                              // Velocità di rotazione 
    [TabGroup("Quaternion")]
    public float rotation = -45f;                                             // Angolo della rotazione
    [TabGroup("Quaternion")]
    [HideInInspector] public float underworldRotate = 0f;                   // Angolo di rotazione del sottosopra

    [TabGroup("Altro")]
    [InfoBox("La telecamera segue il giocatore")]
    public bool isFollow;                                             // La telecamera segue il giocatore

    public bool isShadows;

    [FoldoutGroup("Audio")]
    public AudioClip rotateSound;                                           // Suono della rotazione della stanza

    [Header("Debug")]
    [HideInInspector] public bool isRotating0;                              // La sua rotazione è (0)
    [HideInInspector] public bool isRotating180;                            // La sua rotazione è (180)
    [HideInInspector] public bool isActive;                                 // La camera è attiva



    void Start () {

        rotation = -45;
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        source = GetComponent<AudioSource>();

        arrow.qto = Quaternion.Euler(45, 0, 0);                             // Inizializza il quaternione della freccia
    }
	
	void Update () {

        // LA TELECAMERA SEGUE IL GIOCATORE

        if (isFollow == true)
        {
            transform.position = player.transform.position;   
            
        }

        // SOPRAMONDO E SOTTOMONDO

        if (player.isUnderworld == false)
        {
            if (isShadows)
            {
                QualitySettings.shadows = ShadowQuality.All;                        // Abilita le Ombre
            }


            underworldRotate = 0;
            qto = Quaternion.Euler(0, rotation, underworldRotate);
        }

        if (player.isUnderworld == true)
        {
            if (isShadows)
            {
                QualitySettings.shadows = ShadowQuality.Disable;                    // Disbilita le Ombre
            }


            underworldRotate = 180;
            qto = Quaternion.Euler(0, rotation, underworldRotate);
        }

        // ROTAZIONE DELLA CAMERA

        if(isActive == true)
        {
            // SE LA ROTAZIONE != 0

            if ((Input.GetAxis("Mouse ScrollWheel") > 0f) && rotation != -45)
            {
                source.PlayOneShot(rotateSound);                                // Audio
                gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
                StartCoroutine(Rotate0());                                      // RUOTA 0
            }

            // SE LA ROTAZIONE != 180

            if ((Input.GetAxis("Mouse ScrollWheel") < 0f) && rotation != 135)
            {
                source.PlayOneShot(rotateSound);                                // Audio
                gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
                StartCoroutine(Rotate180());                                    // RUOTA 180
            }

            // SE LA ROTAZIONE != 90

            if (Input.GetKeyDown(KeyCode.LeftArrow) && rotation != 90)
            {
                source.PlayOneShot(rotateSound);                                // Audio
                gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
                StartCoroutine(Rotate90());                                     // RUOTA 90

            }

            // SE LA ROTAZIONE != -90

            if (Input.GetKeyDown(KeyCode.RightArrow) && rotation != -90)
            {
                source.PlayOneShot(rotateSound);                                // Audio
                gameManager.rotationCount++;                                    // Aggiunge 1 alla conta delle rotazioni
                StartCoroutine(RotateLess90());                                 // RUOTA -90
            }
        }

        // QUATERNIONI DELLA CAMERA E DELLA FRECCIA

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
        qto = Quaternion.Euler(0, rotation, underworldRotate);
        yield return new WaitForSeconds(0.4f);
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
        qto = Quaternion.Euler(0, rotation, underworldRotate);
        yield return new WaitForSeconds(0.4f);
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


        rotation = -45;

        qto = Quaternion.Euler(0, rotation, underworldRotate);
        yield return new WaitForSeconds(0.4f);
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

        rotation = 135;
        qto = Quaternion.Euler(0, rotation, underworldRotate);
        yield return new WaitForSeconds(0.4f);
        isActive = true;
        player.isActive = true;
    }
}
