using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //------Maze Related------//
    public Maze mazePrefab;
    public Maze mazeInstance;
    public Text numRLR, numLRL, numUDU, numDUD;
    private short LRL = 0, RLR = 0, UDU = 2, DUD = 0;
    private IntVector2 size = new IntVector2(12, 12);

    //------Ball Related------//
    public Ball ballPrefab;
    public Ball ballInstance;
    
    //------Menu Related------//
    public short menuState = 0; // 0 = Start Screen/ 1 = Mode Selection/ 2 = Controls Selection/ 3 = Hide Menu(In Game)
    public short mode; // 0 = normal/ 1 = hard/ 2 = insane
    public short controls; // 0 = tilt/ 1 = rehab

    //------Not Related------//
    public short pickUps = 0, pickUpsCollected = 0;
    private CameraManager cameraManager;
    public short cameraDistance = 0;
    public Light light;

	// Use this for initialization
	void Start () 
    {

        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();

        if (PlayerPrefs.HasKey("Mode"))
        {
            mode = (short)PlayerPrefs.GetInt("Mode");
        }
        else
        {
            mode = 0;
        }

        if (PlayerPrefs.HasKey("Controls"))
        {
            controls = (short)PlayerPrefs.GetInt("Controls");
        }
        else
        {
            controls = 0;
        }
        
        
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () 
    {

        numRLR.text = "RLR = " + RLR.ToString();
        numLRL.text = "LRL = " + LRL.ToString();
        numUDU.text = "UDU = " + UDU.ToString();
        numDUD.text = "DUD = " + DUD.ToString();

	}

    private void BeginGame() 
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;

        if (menuState == 3)
        {
            mazeInstance.size = size;
            mazeInstance.RLR = RLR;
            mazeInstance.LRL = LRL;
            mazeInstance.UDU = UDU;
            mazeInstance.DUD = DUD;
            mazeInstance.Generate();
            CreatePlayer();
            ballInstance.GetComponent<Movement>().controls = controls;
        }
        else
        {
            mazeInstance.menuState = menuState;
            mazeInstance.size = new IntVector2(23, 19);
            StartCoroutine(mazeInstance.GenerateStepByStep());
        }

        
       
    }

    public void RestartGame() 
    {
        
        Camera.main.transform.parent = null;
        if(ballInstance)
        {
            Destroy(ballInstance.gameObject);
        }
        StopAllCoroutines();
        pickUps = 0;
        
        Destroy(mazeInstance.gameObject);
        BeginGame();
        cameraManager.AdjustCameraPosition();
        
    }

    public void BackToMainMenu()
    {
        menuState = 0;
        RestartGame();
    }

    private void CreatePlayer()
    {
        Vector3 startPosition = mazeInstance.GetCell(new IntVector2(mazeInstance.size.x - 1, 0)).transform.position;
        ballInstance = Instantiate(ballPrefab, startPosition, ballPrefab.transform.rotation) as Ball;
    }

    public void plusLRL()
    {
        LRL += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    public void minusLRL()
    {
        if (LRL > 0)
        {
            LRL -= 1;
        }

        if (size.x > 12)
        {
            size.x -= 2;
            size.z -= 2;
            cameraDistance -= 2;
        }

    }

    public void plusRLR()
    {
        RLR += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    public void minusRLR()
    {
        if (RLR > 0)
        {
            RLR -= 1;
        }

        if (size.x > 12)
        {
            size.x -= 2;
            size.z -= 2;
            cameraDistance -= 2;
        }
    }

    public void plusUDU()
    {
        UDU += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    public void minusUDU()
    {
        if (UDU > 0)
        {
            UDU -= 1;
        }

        if (size.x > 12)
        {
            size.x -= 2;
            size.z -= 2;
            cameraDistance -= 2;
        }
    }

    public void plusDUD()
    {
        DUD += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    public void minusDUD()
    {
        if (DUD > 0)
        {
            DUD -= 1;
        }

        if (size.x > 12)
        {
            size.x -= 2;
            size.z -= 2;
            cameraDistance -= 2;
        }
    }
}
