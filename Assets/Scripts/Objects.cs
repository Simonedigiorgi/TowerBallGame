using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Objects : MonoBehaviour {

    private PlayerController player;                                                                // PLAYER
    private GameManager gameManager;                                                                // GAMEMANAGER

    public enum OBJECTS { None, EndLevel, Key }
    [FoldoutGroup("Select Object")]
    [HideLabel]
    [InfoBox("Seleziona il comportamento della piattaforma")]
    public OBJECTS objects;

    private float speed = 100;
    private bool isItem;
    private bool levelComplete;

	// Use this for initialization
	void Start () {

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

        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        if(isItem == true)
        {
            transform.position = player.transform.GetChild(1).gameObject.transform.position;
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

        if (other.gameObject.tag == "Player")
        {
            if (objects == OBJECTS.Key)
            {
                isItem = true;
            }

            if (objects == OBJECTS.EndLevel)
            {
                levelComplete = true;
            }


        }
    }


}
