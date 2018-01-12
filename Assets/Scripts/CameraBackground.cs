using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour {

    private PlayerController player;
    public Light light;

    public Color color1;
    public Color color2;

    public Color colorLight1;
    public Color colorLight2;
    private float duration = 2f;

	void Start () {
        player = FindObjectOfType<PlayerController>();
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

    }
	
	void Update () {

        if(player.isUnderworld == true)
        {
            light.color = Color.Lerp(light.color, colorLight1, 4f * Time.deltaTime);
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, color1, 4f * Time.deltaTime);

        }
        else
        {
            light.color = Color.Lerp(light.color, colorLight2, 4f * Time.deltaTime);
            GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, color2, 4f * Time.deltaTime);

        }
	
	}


}
