using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Ball ballPrefab;

    private Ball ballInstance;

    public Text numRLR, numLRL, numUDU, numDUD;

    private int LRL = 0, RLR = 0, UDU = 2, DUD = 0;

    public int pickUps = 0, pickUpsCollected = 0;

    private int cameraDistance = 0;

    private IntVector2 size = new IntVector2(12,12);

    public int menuState = 0; // 0 = Start Screen/ 1 = Mode Selection/ 2 = Controls Selection/ 3 = Hide Menu

    

	// Use this for initialization
	void Start () 
    {
        BeginGame();
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        numRLR.text = "RLR = " + RLR.ToString();
        numLRL.text = "LRL = " + LRL.ToString();
        numUDU.text = "UDU = " + UDU.ToString();
        numDUD.text = "DUD = " + DUD.ToString();

	}

    private void BeginGame() 
    {
        if (menuState == 3)
        {
            mazeInstance = Instantiate(mazePrefab) as Maze;
            mazeInstance.size = size;
            mazeInstance.RLR = RLR;
            mazeInstance.LRL = LRL;
            mazeInstance.UDU = UDU;
            mazeInstance.DUD = DUD;
            mazeInstance.Generate();
            Camera.main.transform.position = mazeInstance.cameraPosition.transform.position + new Vector3(0, cameraDistance, 0);
            Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            Camera.main.transform.parent = mazeInstance.cameraPosition.transform;
            CreatePlayer();
        }
        else
        {
            mazeInstance = Instantiate(mazePrefab) as Maze;
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
