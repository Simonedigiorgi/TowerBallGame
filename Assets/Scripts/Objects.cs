using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Objects : MonoBehaviour {

    private PlayerController player;                                                                // PLAYER
    private GameManager gameManager;                                                                // GAMEMANAGER
    private Rigidbody rb;

    public enum OBJECTS { None, EndLevel, Key }
    [FoldoutGroup("Select Object")]
    [HideLabel]
    [InfoBox("Seleziona il comportamento della piattaforma")]
    public OBJECTS objects;

    private float speed = 100;
    public bool isItem;
    private bool levelComplete;

    private bool isGrabbed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();

        if (objects == OBJECTS.None)
        {

        }



        if (objects == OBJECTS.Key)
        {

        }

    }
	
	// Update is called once per frame
	void Update () {

        if (isGrabbed)
        {
            transform.position = player.transform.GetChild(1).gameObject.transform.position;
        }
        else
        {


        }

        //transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if(isItem == true)
        {
            //transform.position = player.transform.GetChild(1).gameObject.transform.position;

            if(Input.GetKeyDown(KeyCode.R) && isGrabbed == false)
            {
                isGrabbed = !isGrabbed;
                /*isItem = false;
                GetComponent<BoxCollider>().isTrigger = false;
                rb.isKinematic = false;
                rb.AddForce(player.transform.right * 150);*/               
            }
            else if(Input.GetKeyDown(KeyCode.T) && isGrabbed == true)
            {
                isGrabbed = !isGrabbed;
                //GetComponent<BoxCollider>().isTrigger = false;
                rb.isKinematic = false;


                if (player.transform.GetChild(0).transform.rotation.y >= 90)
                {
                    rb.AddForce(transform.position.x * 15, transform.position.y, transform.position.z);
                }

            }

        }

        if (objects == OBJECTS.EndLevel)
        {
            if (levelComplete == true)
            {
                transform.position = player.transform.GetChild(1).gameObject.transform.position;
                
                StartCoroutine(gameManager.LevelComplete());
            }

        }

    }


    public void OnTriggerEnter(Collider other)
    {

        if (objects == OBJECTS.Key && other.gameObject.tag == "Player")
        {
                isItem = true;
                /*if (Input.GetKeyDown(KeyCode.E))
                {
                    
                }*/

            


            if (objects == OBJECTS.EndLevel)
            {
                levelComplete = true;
            }


        }
        else
        {
            isItem = false;
        }
    }


}
