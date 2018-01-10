using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private AudioSource source;

    public AudioClip rotateSound;

    public Rotation arrow;                                                   // Richiama lo script Rotation della freccia

    public bool isActive;

    public float speed = 300f;                                              // Velocità di rotazione
    public float rotation = 0f;                                             // Angolo della rotazione
    public float underRotation = 0f;
    public bool isLateralButtons;
    public bool isFrontalButtons;
    public Quaternion qto = Quaternion.identity;

    public PlayerController player;
    public GameManager gameManager;

    [HideInInspector] public bool is0;
    [HideInInspector] public bool is180;


	void Start () {

        isLateralButtons = true;
        source = GetComponent<AudioSource>();
    }
	
	void Update () {     
        
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
        arrow.qto = Quaternion.Euler(0, 0, -90);
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

        arrow.qto = Quaternion.Euler(0, 0, 90);

        isActive = false;
        rotation = 90;
        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }

    public IEnumerator Rotate0()
    {

        is180 = true;
        is0 = false;

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.isActive = false;
        player.qto = Quaternion.Euler(0, 0, 0);

        arrow.qto = Quaternion.Euler(0, 0, 0);

        isActive = false;


        rotation = 0;

        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }

    public IEnumerator Rotate180()
    {
        is0 = true;
        is180 = false;

        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.isActive = false;
        player.qto = Quaternion.Euler(0, rotation, 180);

        arrow.qto = Quaternion.Euler(0, 0, 180);

        isActive = false;

        rotation = 180;
        qto = Quaternion.Euler(0, rotation, underRotation);
        yield return new WaitForSeconds(0.5f);
        isActive = true;
        player.isActive = true;
    }
}
