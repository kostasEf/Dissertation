using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    //------Camera Related------//
    public Transform cameraMenuPosition;
    public Color cameraBackGroundColor;

    //------Not Related------//
    private GameManager gameManager;
    public Light spotLight;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if ((gameManager.mode == 1 || gameManager.mode == 2) && gameManager.menuState == 3)
        {
            transform.position = gameManager.ballInstance.transform.position + new Vector3(0, 10, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.parent = gameManager.mazeInstance.cameraNomralPosition;
        }
    }

    public void AdjustCameraPosition()
    {
        if (gameManager.menuState != 3)
        {
            transform.position = cameraMenuPosition.position;
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
        else if (gameManager.mode == 0 && gameManager.menuState == 3)
        {
            transform.position = gameManager.mazeInstance.cameraNomralPosition.transform.position +
                new Vector3(0, gameManager.cameraDistance, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            transform.parent = gameManager.mazeInstance.cameraNomralPosition;
        }

        if (gameManager.mode == 2 && gameManager.menuState == 3)
        {
            TurnOffLight();
        }
        else
        {
            TurnOnLight();
        }
    }

    public void TurnOffLight()    {
        gameManager.light.intensity = 0;
        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
        RenderSettings.reflectionBounces = 2;
        Camera.main.backgroundColor = Color.black;

        spotLight.intensity = 2;
    }

    public void TurnOnLight()
    {
        gameManager.light.intensity = 1;
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
        Camera.main.backgroundColor = cameraBackGroundColor;
        spotLight.intensity = 0;
    }
	
}
