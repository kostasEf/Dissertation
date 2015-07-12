using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    public IntVector2 size;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public MazePassage passagePrefab;

    public MazeWall wallPrefab;

    public Block_Test blockPrefab;

	// Use this for initialization
	void Start () {
        
        
	}

    private void InitializeBlock(Block_Test block)
    {
        CreateCell(new IntVector2(1, 1));
        CreateCell(new IntVector2(1, 2));
        CreateCell(new IntVector2(1, 3));
        CreateCell(new IntVector2(2, 1));
        CreateCell(new IntVector2(2, 2));
        CreateCell(new IntVector2(2, 3));
        CreateCell(new IntVector2(3, 1));
        CreateCell(new IntVector2(3, 2));
        CreateCell(new IntVector2(3, 3));

        cells[1, 1].FullyInitialize();
        cells[1, 2].FullyInitialize();
        cells[1, 3].FullyInitialize();
        cells[2, 1].FullyInitialize();
        cells[2, 2].FullyInitialize();
        cells[2, 3].FullyInitialize();
        cells[3, 1].FullyInitialize();
        cells[3, 2].FullyInitialize();
        cells[3, 3].FullyInitialize();

        cells[3, 3].isEntryPoint = true;

        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];

        Block_Test block = Instantiate(blockPrefab) as Block_Test;
        InitializeBlock(block);

        
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    //public void Generate()
    //{
    //    cells = new MazeCell[size.x, size.z];
    //    List<MazeCell> activeCells = new List<MazeCell>();
    //    DoFirstGenerationStep(activeCells);
    //    while (activeCells.Count > 0)
    //    {
    //        DoNextGenerationStep(activeCells);
    //    }
    //}

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
    making sure it does not belong inside one of the blocks
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

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
}
