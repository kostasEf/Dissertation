using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //------Maze Related------//
    public Maze mazePrefab;
    public Maze mazeInstance;
    private short LRL = 0, RLR = 0, UDU = 2, DUD = 0;
    private IntVector2 size = new IntVector2(12, 12);

    //------Ball Related------//
    public Ball ballPrefab;
    public Ball ballInstance;
    
    //------Menu Related------//
    public short menuState = 0; // 0 = Start Screen/ 1 = Mode Selection/ 2 = Controls Selection/ 3 = Hide Menu(In Game)
    public short mode; // 0 = normal/ 1 = hard/ 2 = insane
    public short controls; // 0 = tilt/ 1 = rehab
    public GameObject gameOver;
    private bool pause = false;
    private MenuManager menuManager;

    //------Timer Related------//
    public float timer;
    private float seconds;
    private float minutes;

    //------Not Related------//
    public short pickUps = 0, pickUpsCollected = 0;
    private CameraManager cameraManager;
    public short cameraDistance = 0;
    public Light light;
    public Text time;
    

	// Use this for initialization
	void Start () 
    {

        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();

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
        Timer();

        if (timer < 0)
        {
            timer = 0;
            GameOver();
        }
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
        if (ballInstance)
        {
            Destroy(ballInstance.gameObject);
        }
        if (mazeInstance)
        {
            Destroy(mazeInstance.gameObject);
        }
        StopAllCoroutines();
        pickUps = 0;
        pickUpsCollected = 0;
        BeginGame();
        
        cameraManager.AdjustCameraPosition();
        
    }

    public void Pause()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }

    public void Timer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60);
            seconds = timer % 60;
            time.text = "Time \n" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        

       
    }

    public void BackToMainMenu()
    {
        menuState = 0;
        pause = false;
        RestartGame();
    }

    public void GameOver()
    {
        Camera.main.transform.parent = null;
        if (ballInstance)
        {
            Destroy(ballInstance.gameObject);
        }
        if (mazeInstance)
        {
            Destroy(mazeInstance.gameObject);
        }
        StopAllCoroutines();

        gameOver.SetActive(true);
        menuManager.disableMenu();
        
    }

    public void NextLVL()
    {
        int random = Random.Range(0, 5);
        if (random == 0)
        {
            plusLRL();
        }
        else if (random == 1)
        {
            plusRLR();
        }
        else if (random == 2)
        {
            plusDUD();
        }
        else if (random == 3)
        {
            plusUDU();
        }

        RestartGame();
    }

    private void CreatePlayer()
    {
        Vector3 startPosition = mazeInstance.GetCell(new IntVector2(mazeInstance.size.x - 1, 0)).transform.position;
        ballInstance = Instantiate(ballPrefab, startPosition, ballPrefab.transform.rotation) as Ball;
    }

    private void plusLRL()
    {
        LRL += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    private void minusLRL()
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

    private void plusRLR()
    {
        RLR += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    private void minusRLR()
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

    private void plusUDU()
    {
        UDU += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    private void minusUDU()
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

    private void plusDUD()
    {
        DUD += 1;

        if ((LRL + RLR + UDU + DUD + 1) > 3)
        {
            size.x += 2;
            size.z += 2;
            cameraDistance += 2;
        }
    }

    private void minusDUD()
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
