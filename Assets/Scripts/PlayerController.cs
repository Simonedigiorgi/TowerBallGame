﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CameraController cameraController;
    public Rotation head;                                                   // Richiama lo script Rotation dealla testa

    public LayerMask groundLayers;

    public float rotSpeed = 300f;                                           // Velocità di rotazione
    public float rotation = 0f;                                             // Angolo della rotazione
    [HideInInspector] public Quaternion qto = Quaternion.identity;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SphereCollider col;

    public float speed = 1;
    public int jump = 9;

    [Header("Debug")]
    public bool isUnderworld;
    public bool isActive;
    public bool isJump;


    void Start () {

        isJump = true;
        isActive = true;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
	}
	
	void Update () {

        head.qto = Quaternion.Euler(0, head.rotation, 0);

        // RUOTA A 90

        if (cameraController.rotation == 90 && isActive == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                head.rotation = 90;
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            { 
                head.rotation = -90;
                transform.Translate(0, 0, -speed * Time.deltaTime);
            }
        }

        // RUOTA A -90

        if(cameraController.rotation == -90 && isActive == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                head.rotation = -90;
                transform.Translate(0, 0, -speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                head.rotation = 90;
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

        }

        // RUOTA A 0

        if(cameraController.rotation == 0 && isActive == true)
        {

            if(isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
            }
        }

        // RUOTA A 180

        if (cameraController.rotation == 180 && isActive == true)
        {
            if (isUnderworld == true)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    head.rotation = 90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    head.rotation = -90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }
            }

            if (isUnderworld == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    head.rotation = 90;
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    head.rotation = -90;
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                }
            }

        }

        // UNDERWORLD AND JUMP

        if (isUnderworld == true)
        {
            if (UnderGrounded() && Input.GetKeyDown(KeyCode.Space) && isJump == true)
            {
                rb.AddForce(Vector3.down * jump, ForceMode.Impulse);
            }

            Physics.gravity = new Vector3(0, 9.81f, 0);
        }

        else if (isUnderworld == false)
        {
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && isJump == true)
            {
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }

            Physics.gravity = new Vector3(0, -9.81f, 0);
        }

        head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, head.qto, head.rotSpeed * Time.deltaTime);


    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .1f, groundLayers);
    }

    private bool UnderGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.max.y, col.bounds.center.z), col.radius * .1f, groundLayers);
    }
}
