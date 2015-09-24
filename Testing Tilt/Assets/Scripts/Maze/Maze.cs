using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    //------Maze Related------//
    private MazeCell[,] cells;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public IntVector2 size;
    public float generationStepDelay;


    //------Block Related------//
    public Block_LRL blockLRL;
    public Block_RLR blockRLR;
    public Block_DUD blockDUD;
    public Block_UDU blockUDU;
    public Block_Spiral blockSpiral;
    public short RLR, LRL, UDU, DUD;
    public Play blockPlay;
    public Mode blockMode;
    public Controls blockControls;
    public Normal blockNormal;
    public Hard blockHard;
    public Insane blockInsane;
    public Rehab blockRehab;

    //------Not Related------//
    public Transform cameraNomralPosition;
    public PickUp pickUpPrefab;
    public short menuState = 0; // 0 = Start Screen/ 1 = Mode Selection/ 2 = Controls Selection/ 3 = Hide Menu(In Game)

    

	// Use this for initialization
    void Start() {  }

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

        PlaceRandomPickUps();

    }

    private void PlaceRandomPickUps()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.z; j++)
            {
                if (cells[i, j].IsDeadEnd() && cells[i, j].belongsToBlock == false)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        PickUp pickUp = Instantiate(pickUpPrefab, cells[i, j].transform.position, Quaternion.identity) as PickUp;
                        pickUp.transform.parent = transform;
                        //gameManager.extraPickUps++;
                    }
                }
            }
        }
    }

    public IEnumerator GenerateStepByStep()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        CreateMenuRooms();
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
        FindEmptyCells();
    }

    private void CreateMenuRooms()
    {
        int x = 4;
        int y = 2;

        for (int i = 11 - x; i <= 11 + x; i++)
        {
            for (int j = 15 - y; j <= 15 + y; j++)
            {
                CreateCell(new IntVector2(i, j));
                cells[i, j].FullyInitializeEdges();
                cells[i, j].belongsToBlock = true;
            }
        }

        for (int i = 11 - x; i <= 11 + x; i++)
        {
            for (int j = 9 - y; j <= 9 + y; j++)
            {
                CreateCell(new IntVector2(i, j));
                cells[i, j].FullyInitializeEdges();
                cells[i, j].belongsToBlock = true;
            }
        }

        if (menuState == 0 || menuState == 1)
        {
            for (int i = 11 - x; i <= 11 + x; i++)
            {
                for (int j = 3 - y; j <= 3 + y; j++)
                {
                    CreateCell(new IntVector2(i, j));
                    cells[i, j].FullyInitializeEdges();
                    cells[i, j].belongsToBlock = true;
                }
            }
        }
        


        if (menuState == 0)
        {
            Play block = Instantiate(blockPlay, cells[11, 15].transform.position, Quaternion.identity) as Play;
            block.transform.parent = transform;

            Mode block2 = Instantiate(blockMode, cells[11, 9].transform.position, Quaternion.identity) as Mode;
            block2.transform.parent = transform;

            Controls block3 = Instantiate(blockControls, cells[11, 3].transform.position, Quaternion.identity) as Controls;
            block3.transform.parent = transform;
        }
        if (menuState == 1)
        {
            Normal block = Instantiate(blockNormal, cells[11, 15].transform.position, Quaternion.identity) as Normal;
            block.transform.parent = transform;

            Hard block2 = Instantiate(blockHard, cells[11, 9].transform.position, Quaternion.identity) as Hard;
            block2.transform.parent = transform;

            Insane block3 = Instantiate(blockInsane, cells[11, 3].transform.position, Quaternion.identity) as Insane;
            block3.transform.parent = transform;
        }
        if (menuState == 2)
        {
            Normal block = Instantiate(blockNormal, cells[11, 15].transform.position, Quaternion.identity) as Normal;
            block.transform.parent = transform;

            Rehab block2 = Instantiate(blockRehab, cells[11, 9].transform.position, Quaternion.identity) as Rehab;
            block2.transform.parent = transform;                        
        }



        

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
