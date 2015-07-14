using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Ball ballPrefab;

    private Ball ballInstance;

    public Text sizeX, sizeZ;

    private IntVector2 size = new IntVector2(15,15);



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

        sizeX.text = "X = " + size.x.ToString();
        sizeZ.text = "Z = " + size.z.ToString();
	}

    private void BeginGame() 
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.size = size;
        //StartCoroutine(mazeInstance.Generate());
        mazeInstance.Generate();
        //CreatePlayer();
        
    }

    public void RestartGame() 
    {
        //StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        //Destroy(ballInstance.gameObject);
        BeginGame();
    }

    private void CreatePlayer()
    {
        Vector3 startPosition = mazeInstance.GetCell(new IntVector2(mazeInstance.size.x - 1, 0)).transform.position;
        ballInstance = Instantiate(ballPrefab, startPosition, ballPrefab.transform.rotation) as Ball;
    }

    public void addX()
    {
        size.x += 1;
    }

    public void addZ()
    {
        size.z += 1;
    }

    public void minusX()
    {
        size.x -= 1;
    }

    public void minusZ()
    {
        size.z -= 1;
    }
}
