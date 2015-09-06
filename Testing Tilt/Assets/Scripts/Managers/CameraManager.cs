using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    //------Camera Related------//
    public Transform cameraMenuPosition;
    public Color cameraBackGroundColor;

    //------Not Related------//
    private GameManager GameManager;
    public Light spotLight;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if ((GameManager.mode == 1 || GameManager.mode == 2) && GameManager.ballInstance)
        {
            transform.position = GameManager.ballInstance.transform.position + new Vector3(0, 10, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.parent = GameManager.mazeInstance.cameraNomralPosition;
        }      
    }

    public void AdjustCameraPosition()
    {
        if (GameManager.menuState != 3)
        {
            transform.position = cameraMenuPosition.position;
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
        else if (GameManager.mode == 0 && GameManager.ballInstance)
        {
            transform.position = GameManager.mazeInstance.cameraNomralPosition.transform.position +
                new Vector3(0, GameManager.cameraDistance, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.parent = GameManager.mazeInstance.cameraNomralPosition;
        }

        if (GameManager.mode == 2 && GameManager.menuState == 3)
        {
            TurnOffLight();
        }
        else
        {
            TurnOnLight();
        }
    }

    public void TurnOffLight()    {
        GameManager.light.intensity = 0;
        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
        RenderSettings.reflectionBounces = 2;
        Camera.main.backgroundColor = Color.black;

        spotLight.intensity = 2;
    }

    public void TurnOnLight()
    {
        GameManager.light.intensity = 1;
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
        Camera.main.backgroundColor = cameraBackGroundColor;
        spotLight.intensity = 0;
    }
	
}
