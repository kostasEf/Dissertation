﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    public IntVector2 size;

    public GameObject CameraPosition;

    public int RLR, LRL, UDU, DUD;

    public MazeCell cellPrefab;

    public PickUp pickUpPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public MazePassage passagePrefab;

    public MazeWall wallPrefab;

    public Block_LRL blockLRL;
    public Block_RLR blockRLR;
    public Block_DUD blockDUD;
    public Block_UDU blockUDU;
    public Block_Spiral blockSpiral;


	// Use this for initialization
	void Start () {}

    /*
     * First check all cells where the room is going to be placed to see 
     * if they are available, then create the cells and FullyInitialize them.   
    */
    private void InitializeRoom(IntVector2 coordinates, int roomType)
    {
        
        for (int i = coordinates.x - 1; i <= coordinates.x + 1; i++)
        {
            for (int j = coordinates.z - 1; j <= coordinates.z + 1; j++)
            {
                CreateCell(new IntVector2(i, j));
                cells[i, j].FullyInitializeEdges();
                cells[i, j].belongsToBlock = true;
            }
        }

        Vector3 position = cells[coordinates.x, coordinates.z].transform.position;
        
        if (roomType == 1)
        {
            Block_RLR block = Instantiate(blockRLR, position, Quaternion.identity) as Block_RLR;
            block.transform.parent = transform;

            cells[coordinates.x - 1, coordinates.z - 1].isEntryPoint = true;
            cells[coordinates.x + 1, coordinates.z + 1].isEntryPoint = true;
        }
        else if (roomType == 2)
        {
            Block_LRL block = Instantiate(blockLRL, position, Quaternion.identity) as Block_LRL;
            block.transform.parent = transform;

            cells[coordinates.x - 1, coordinates.z + 1].isEntryPoint = true;
            cells[coordinates.x + 1, coordinates.z - 1].isEntryPoint = true;
        }
        else if (roomType == 3)
        {
            Block_UDU block = Instantiate(blockUDU, position, Quaternion.identity) as Block_UDU;
            block.transform.parent = transform;

            cells[coordinates.x - 1, coordinates.z - 1].isEntryPoint = true;
            cells[coordinates.x + 1, coordinates.z + 1].isEntryPoint = true;
        }
        else if (roomType == 4)
        {
            Block_DUD block = Instantiate(blockDUD, position, Quaternion.identity) as Block_DUD;
            block.transform.parent = transform;

            cells[coordinates.x - 1, coordinates.z + 1].isEntryPoint = true;
            cells[coordinates.x + 1, coordinates.z - 1].isEntryPoint = true;
        }
        else if (roomType == 5)
        {
            Block_Spiral block = Instantiate(blockSpiral, position, Quaternion.identity) as Block_Spiral;
            block.transform.parent = transform;

            cells[coordinates.x - 1, coordinates.z - 1].isEntryPoint = true;
        }

        PlacePickUp(position);
        

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    //public IEnumerator Generate()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
    //    cells = new MazeCell[size.x, size.z];

    //    //Block_Test block = Instantiate(blockPrefab) as Block_Test;
    //    bool roomIsPlaced = false;

    //    while (roomIsPlaced == false)
    //    {
    //        roomIsPlaced = InitializeRoom(RandomCoordinates());
    //        //roomIsPlaced = InitializeRoom(new IntVector2(2,2));
    //    }
        

        
    //    List<MazeCell> activeCells = new List<MazeCell>();
    //    DoFirstGenerationStep(activeCells);
    //    while (activeCells.Count > 0)
    //    {
    //        yield return delay;
    //        DoNextGenerationStep(activeCells);
    //    }
    //}

    public void Generate()
    {
        cells = new MazeCell[size.x, size.z];

        PlaceRooms();

        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells);
        }

        FindEmptyCells();
        

    }

    private void PlacePickUp(Vector3 position)
    {
        
        PickUp pickUp = Instantiate(pickUpPrefab, position, Quaternion.identity) as PickUp;
        pickUp.transform.parent = transform;

    }

    private void PlaceRooms()
    {
        for (int i = 0; i < RLR; i++)
        {
            InitializeRoom(RandomRoomCoordinates(), 1);
        }

        for (int i = 0; i < LRL; i++)
        {
            InitializeRoom(RandomRoomCoordinates(), 2);
        }

        for (int i = 0; i < UDU; i++)
        {
            InitializeRoom(RandomRoomCoordinates(), 3);
        }

        for (int i = 0; i < DUD; i++)
        {
            InitializeRoom(RandomRoomCoordinates(), 4);
        }

        InitializeRoom(RandomRoomCoordinates(), 5);
    }

    private void FindEmptyCells()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                if (cells[i, j] == null)
                {
                    CreateCell(new IntVector2(i, j));
                    cells[i, j].FullyInitializeEdges();
                }
                    
                
            }
        }

        
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates()));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if(neighbor.isEntryPoint)
            {
                CreatePassage(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }



    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    /*
    Choose a random coordinate to place the first cell
    making sure it does not belong inside one of the rooms
    */
    public IntVector2 RandomCoordinates()
    {
        
            IntVector2 randomCoord = new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
            while (cells[randomCoord.x, randomCoord.z] && cells[randomCoord.x, randomCoord.z].IsFullyInitialized)
            {
                randomCoord = new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
            }
            return randomCoord;
        
    }

    public IntVector2 RandomRoomCoordinates()
    {
        int count = 0;
        int tries = 0;
        int gapFromEdge = 2;
        int searchOffset = 2;
        IntVector2 randomRoomCoord = new IntVector2(Random.Range(gapFromEdge, size.x - gapFromEdge), Random.Range(gapFromEdge, size.z - gapFromEdge));
        while (count != 25)
        {
            count = 0;
            tries++;
            randomRoomCoord = new IntVector2(Random.Range(gapFromEdge, size.x - gapFromEdge), Random.Range(gapFromEdge, size.z - gapFromEdge));
            for (int i = randomRoomCoord.x - searchOffset; i <= randomRoomCoord.x + searchOffset; i++)
            {
                for (int j = randomRoomCoord.z - searchOffset; j <= randomRoomCoord.z + searchOffset; j++)
                {
                    if (cells[i, j] == null)
                    {
                        count++;
                    }
                }
            }
        }

        return randomRoomCoord;

    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction, false);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite(), false);
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction, true);

        if (otherCell != null)
        {
            //wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite(), true);
        }
    }
}
