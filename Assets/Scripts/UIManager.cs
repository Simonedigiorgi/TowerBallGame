using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [Header("Buttons")]
    public Button startGame;
    public Button quit;

    [Header("Images")]
    public Image fadeImage;                                                     // Immagine di Fade

    


    void Start () {
        StartCoroutine(FadeOut());
    }
	
	void Update () {       

    }

    public void StartGame()
    {
        StartCoroutine(STARTGAME());
    }

    public void Quit()
    {
        Debug.Log("Quit");
    }

    public IEnumerator STARTGAME()
    {
        fadeImage.enabled = true;
        fadeImage.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Tutorial_1");
    }

    public IEnumerator FadeOut()
    {
        fadeImage.enabled = true;

        yield return new WaitForSeconds(2);  
        
        fadeImage.DOFade(0, 1);

        yield return new WaitForSeconds(0.8f);

        fadeImage.enabled = false;
        startGame.interactable = true;
        quit.interactable = true;
    }
}
