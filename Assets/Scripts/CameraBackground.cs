using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CameraBackground : MonoBehaviour {

    private PlayerController player;                                            // PLAYER CONTROLLER
    private Light directionalLight;                                             // Luce Direzionale (Child(0))

    [BoxGroup("Sfondo e luce del Sopramondo")]
    public Color worldColor, worldLight;                                        // Colori della luce e dello sfondo nel Sopramondo

    [BoxGroup("Sfondo e luce del Sottomondo")]
    public Color underworldColor, underworldLight;                              // Colori della luce e dello sfondo nel Sottomondo

    [FoldoutGroup("Colore dei testi")] public Text rotationText;                // Testo della rotazione
    [FoldoutGroup("Colore dei testi")] public Text currentLevel;                // Testo del livello corrente
    [FoldoutGroup("Colore dei testi")] public Text description;                 // Testo della descrizione
    [FoldoutGroup("Colore dei testi")] public Text timerText;                   // Testo del timer
    [FoldoutGroup("Colore dei testi")] public Text changeText;                  // Testo del cambio prospettiva

    [FoldoutGroup("Colore dei testi")] public Color textColor;                  // Colore del Testo

    [BoxGroup("Camera Field of View")]
    public float TargetFOV, SpeedFOV;                                           // Distanza e velocita della Field of View

    void Start () {

        player = FindObjectOfType<PlayerController>();
        directionalLight = transform.GetChild(0).GetComponent<Light>();
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;       
    }
	
	void Update () {

        // CAMERA FOV

        Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, TargetFOV, Time.deltaTime * SpeedFOV);

        // COLORI E LUCI

        if (player.isUnderworld == true)
        {
            directionalLight.color = Color.Lerp(directionalLight.color, underworldLight, 4f * Time.deltaTime);
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, underworldColor, 4f * Time.deltaTime);

            // TESTI

            rotationText.color = Color.white;
            currentLevel.color = Color.white;
            description.color = Color.white;
            timerText.color = Color.white;
            changeText.color = Color.white;

        }

        else if (player.isUnderworld == false)
        {
            directionalLight.color = Color.Lerp(directionalLight.color, worldLight, 4f * Time.deltaTime);
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, worldColor, 4f * Time.deltaTime);

            // TESTI

            rotationText.color = Color.black;
            currentLevel.color = Color.black;
            description.color = Color.black;
            timerText.color = Color.black;
            changeText.color = Color.black;
        }
	}
}
