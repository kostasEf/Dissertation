﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Ball ballPrefab;

    private Ball ballInstance;

    public Text sizeX, sizeZ;

    private int cubes = 2, spheres = 1;

    private IntVector2 size = new IntVector2(10,10);



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

        sizeX.text = "Cubes = " + cubes.ToString();
        sizeZ.text = "Spheres = " + spheres.ToString();
	}

    private void BeginGame() 
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        mazeInstance.size = size;
        mazeInstance.cubes = cubes;
        mazeInstance.spheres = spheres;

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

    public void plusCube()
    {
        cubes += 1;

        if ( (cubes + spheres) > 3)
        {
            size.x += 2;
            size.z += 2;
        }
    }

    public void plusSphere()
    {
        spheres += 1;

        if ((cubes + spheres) > 3)
        {
            size.x += 2;
            size.z += 2;
        }
    }

    public void minusCube()
    {
        if (cubes > 0)
        {
            cubes -= 1;
        }

        if (size.x > 10)
        {
            size.x -= 2;
            size.z -= 2;
        }
        
    }

    public void minusSphere()
    {
        if (spheres > 0)
        {
            spheres -= 1;
        }

        if (size.x > 10)
        {
            size.x -= 2;
            size.z -= 2;
        }
    }
}
