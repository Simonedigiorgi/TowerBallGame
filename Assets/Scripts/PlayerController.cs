using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PlayerController : MonoBehaviour {
   
    [HideInInspector] public Quaternion qto = Quaternion.identity;            
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SphereCollider col;
    [HideInInspector] public LayerMask groundLayers;                          // Layer di Default (Togliere HideInIspector per selezionare un altro LayerMask)

    private CameraController cameraController;                               // CAMERACONTROLLER
    private Rotation head;                                                   // Richiama lo script Rotation dealla testa

    [TabGroup("Player")]
    public float speed = 1f, jump = 10f;                                     // Velocità // Salto

    [TabGroup("Quaternion")]
    public float rotSpeed = 300f, rotation = 0f;                            // Velocità di rotazione // Angolo della rotazione

    [FoldoutGroup("Lerp Colors")] public Color worldSkin, underworldSkin;
    [FoldoutGroup("Lerp Colors")] public float lerpDuration;
    [FoldoutGroup("Lerp Colors")] public bool isLerpActive;

    private float startTime;
    
    [FoldoutGroup("Debug")] [ToggleLeft] public bool isActive, isUnderworld, isJump, isVericalMovements, isHorizontalMovements, onGround;

    void Start () {

        onGround = true;

        startTime = Time.time;

        isActive = true;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        head = GetComponentInChildren<Rotation>();
        cameraController = FindObjectOfType<CameraController>();



    }
	
	void Update () {

        head.qto = Quaternion.Euler(0, head.rotation, 0);

        #region MATERIAL LERP

        if (isLerpActive == true)
        {
            if (isUnderworld == true)
            {
                transform.GetChild(0).GetComponent<Renderer>().material.DOColor(underworldSkin, lerpDuration);
            }
            else if (isUnderworld == false)
            {
                transform.GetChild(0).GetComponent<Renderer>().material.DOColor(worldSkin, lerpDuration);
            }
        }
        #endregion

        #region ROTAZIONE -90

        if (cameraController.rotation == 90 && isActive == true)
        {
            if (isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

        }
        #endregion

        #region ROTAZIONE -90

        if(cameraController.rotation == -90 && isActive == true)
        {
            if (isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

        }
        #endregion

        #region ROTAZIONE 0

        if (cameraController.rotation == -45 && isActive == true)
        {

            if (isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 180;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 0;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 0;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 180;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isVericalMovements = true;
                }
            }
        }
        #endregion

        #region ROTAZIONE 180

        if (cameraController.rotation == 135 && isActive == true)
        {

            if (isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 0;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 180;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.W) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 180;
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S) && isVericalMovements == true)
                {
                    isHorizontalMovements = false;
                    head.rotation = 0;
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                else
                {
                    isHorizontalMovements = true;
                }

                if (Input.GetKey(KeyCode.A) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
                else if (Input.GetKey(KeyCode.D) && isHorizontalMovements == true)
                {
                    isVericalMovements = false;
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isVericalMovements = true;
                }
            }

        }
        #endregion

        #region JUMP
        if (isUnderworld == true)
        {
            if (onGround && Input.GetKeyDown(KeyCode.Space) && isJump == true)
            {
                rb.AddForce(Vector3.down * jump, ForceMode.Impulse);
                onGround = false;
            }

            Physics.gravity = new Vector3(0, 9.81f, 0);
        }

        else if (isUnderworld == false)
        {
            if (onGround && Input.GetKeyDown(KeyCode.Space) && isJump == true)
            {
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                onGround = false;
            }

            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        #endregion

        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, head.qto, head.rotSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
}
