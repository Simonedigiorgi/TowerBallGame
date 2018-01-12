using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public enum Level { None, Showcase, Tutorial1, Tutorial2, Tutorial3}
    public Level level;

    //private AudioSource source;
    public PlayerController player;
    public CameraController cameraController;

    [Header("HUD")]
    public Text rotationText;                                               // Testo della rotazione
    public Text currentLevel;                                               // Testo del livello corrente
    public Text description;                                                // Testo della descrizione
    public Text timerText;                                                  // Testo del timer
    public Text changeText;                                                 // Testo del cambio prospettiva
    public Image fadeImage;                                                 // Immagine di Fade

    [Header("Audio")]
    public AudioClip levelCompleteSound;

    [Header("Debug")]
    public int buttonsPushed;                                               // Mostra quanti tasti sono stati premuti
    public int rotationCount;                                               // Mostra quante volte hai ruotato la stanza
    public int timer;                                                       // Mostra il conteggio del timer
    public int changes;                                                     // Mostra il conteggio delle prospettive cambiate


    void Start () {

        //source = GetComponent<AudioSource>();

        if (level == Level.None)
        {
            Debug.Log("None");
        }

        else if(level == Level.Showcase)
        {
            currentLevel.text = "";
            description.text = "SHOWCASE";
            StartCoroutine("StartGame");
        }

        else if (level == Level.Tutorial1)
        {
            currentLevel.text = "";
            description.text = "Usa i comandi " + "A" + " e " + "D" + " per muoverti, Premi SPAZIO per saltare, usa le frecce per ruotare la telecamera";
            changeText.enabled = false;
            StartCoroutine("StartGame");

        }

        else if (level == Level.Tutorial2)
        {
            currentLevel.text = "";
            description.text = "Alcuni oggetti dello scenario interagiscono con la rotazione della telecamera";
            changeText.enabled = false;
            StartCoroutine("StartGame");
        }

        else if (level == Level.Tutorial3)
        {
            currentLevel.text = "";
            description.text = "Le piattaforme ARANCIONI ti permettono di andare nel sottomondo e viceversa, quando ci sei sopra premi S per usarle";
            StartCoroutine("StartGame");
        }

    }
	
	void Update () {

        timerText.text = "Timer:    " + timer;
        rotationText.text = "Rotations:    " + rotationCount;
        changeText.text = "Change:    " + changes;    

    }

    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer++;
        }
    }

    public IEnumerator StartGame()
    {
            cameraController.isActive = false;                                      // Disattiva la telecamera
            player.isActive = false;                                                // Disattiva il giocatore
            fadeImage.enabled = true;                                               // Abilità l'immagine di Fade
            yield return new WaitForSeconds(1.5f);
            fadeImage.DOFade(0, 3);                                                 // Fade OUT
            yield return new WaitForSeconds(3);
            player.isActive = true;                                                 // Attiva il Giocatore
            fadeImage.enabled = false;                                              // Disabilita l'immagine di Fade
            cameraController.isActive = true;                                       // Attiva la telecamera
            StartCoroutine("Countdown");                                            // Inizia il Countdown
    }

    public IEnumerator LevelComplete()
    {

        if (level == Level.Tutorial1)                                               // TUTORIAL 1
        {
            StopCoroutine("Countdown");
            fadeImage.enabled = true;                                               // Abilità l'immagine di Fade
            cameraController.isActive = false;                                      // Disattiva la telecamera
            player.isActive = false;                                                // Disattiva il giocatore
            yield return new WaitForSeconds(2);
            fadeImage.DOFade(1, 3);                                                 // Fade IN               
            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Tutorial_2");
        }

        if (level == Level.Tutorial2)                                               // TUTORIAL 2
        {
            StopCoroutine("Countdown");
            fadeImage.enabled = true;                                               // Abilità l'immagine di Fade
            cameraController.isActive = false;                                      // Disattiva la telecamera   
            player.isActive = false;                                                // Disattiva il giocatore
            yield return new WaitForSeconds(2);
            fadeImage.DOFade(1, 3);                                                 // Fade IN 
            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Tutorial_3");
        }

        if (level == Level.Tutorial3)                                               // TUTORIAL 3
        {
            StopCoroutine("Countdown");
            fadeImage.enabled = true;                                               // Abilità l'immagine di Fade
            cameraController.isActive = false;                                      // Disattiva la telecamera   
            player.isActive = false;                                                // Disattiva il giocatore
            yield return new WaitForSeconds(2);
            fadeImage.DOFade(1, 3);                                                 // Fade IN 
            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Tutorial_1");
        }
    }

    public IEnumerator Restart()
    {
        StopCoroutine("Countdown");
        cameraController.isActive = false;                                          // Disattiva la telecamera   
        fadeImage.enabled = true;                                                   // Abilità l'immagine di Fade
        player.isActive = false;                                                    // Disattiva il giocatore
        yield return new WaitForSeconds(1);
        fadeImage.DOFade(1, 1);                                                     // Fade IN 
        yield return new WaitForSeconds(1);

        if (level == Level.Showcase)
        {
            SceneManager.LoadScene("Showcase");
        }

        if (level == Level.Tutorial1)
        {
            SceneManager.LoadScene("Tutorial_1");
        }

        if (level == Level.Tutorial2)
        {
            SceneManager.LoadScene("Tutorial_2");
        }

        if (level == Level.Tutorial3)
        {
            SceneManager.LoadScene("Tutorial_3");
        }



    }



}
