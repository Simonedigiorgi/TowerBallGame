using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

    [Header("Components")]
    public GameManager gameManager;                                                                 // GAMEMANAGER

    public enum PushButtons { None, End}
    public PushButtons button;

    private bool isPushed;                                                                          // Hai premuto il tasto

    void Start () {

        transform.DetachChildren();
    }
	
	void Update () {

	}


    public void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.tag == "Player" && isPushed == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            isPushed = true;

            if (button == PushButtons.End)
            {
                gameManager.buttonsPushed++;
                StartCoroutine(gameManager.LevelComplete());
            }

        }
    }

}
