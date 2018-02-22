using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public IntVector2 size;
    public MazeCell cellPrefab;
    public float generationStepDelay;
    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public MazeWall torchWallPrefab;
    public Ghost ghostPrefab1;
    public Campfire campfirePrefab;


    public int startLight;
    public int maxLight;

    [Range(0f, 1f)]
    public float ghostProbabiliy;

    [Range(0f, 1f)]
    public float torchProbability;

    [Range(0f, 1f)]
    public float campfireProbability;

    public int ghostMax;
    public int torchMax;
    public int campfireMax;

    private int torchCount;
    private int campfireCount;


    private MazeCell[,] cells;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public MazeCell GetCell (IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }


    public IEnumerator Generate ()
    {
        Ghost.ghostCount = 0;
        torchCount = 0;
        campfireCount = 0;
        Scoring.SetLightMax(maxLight);
        Scoring.SetLight(startLight);

        Debug.Log("Starting with " + Scoring.GetLight() + " light");

        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }

        /*
          IntVector2 coordinates = RandomCoordinates;
        while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            yield return delay;
            CreateCell(coordinates);
            coordinates += MazeDirections.RandomValue.ToIntVector2();
        }
        */
        /*
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                yield return delay;
                CreateCell(new IntVector2(x, z));
            }
        }
        */
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates (IntVector2 coordinate)
    {
        return (coordinate.x >= 0) && (coordinate.x < size.x) &&
                (coordinate.z >= 0) && (coordinate.z < size.z);
    }

    private MazeCell CreateCell (IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "MazeCell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f,
                                                        coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    private void DoFirstGenerationStep (List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep (List<MazeCell> activeCells)
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
            } else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        } else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if (Random.value < ghostProbabiliy && Ghost.GhostCountGet() < ghostMax)
        {
            CreateGhost(ghostPrefab1, cell);
            Debug.Log("Ghosts: " + Ghost.GhostCountGet());
        } else
        if (Random.value < campfireProbability && campfireCount < campfireMax)
        {
            CreateCampfire(cell);
            Debug.Log("Campfire" + campfireCount + " at " + cell.coordinates.x + ", " + cell.coordinates.z);
        }
    }

    private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall;
        if (Random.value < torchProbability && torchCount < torchMax)
        {
            wall = Instantiate(torchWallPrefab) as MazeWall;
            Debug.Log("Torch: " + torchCount);
            torchCount++;
        } else
        {
            wall = Instantiate(wallPrefab) as MazeWall;
        }
        if (wall == null)
        {
            Debug.LogError("Wall unable to be instantiated. Check prefabs.");
        }
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private void CreateGhost(Ghost gPrefab, MazeCell cell)
    {
        Ghost ghost = Instantiate(gPrefab) as Ghost;
        ghost.name = "Ghost" + Ghost.GhostCountGet();
        ghost.transform.parent = cell.transform;
        ghost.transform.localPosition = new Vector3(0f, 0.0f, 0f);
        Ghost.GhostCountPlus(1); // Sends message that one ghost instantiated
    }

    private void CreateCampfire(MazeCell cell)
    {
        Campfire campfire = Instantiate(campfirePrefab) as Campfire;
        campfire.name = "Campfire" + campfireCount;
        campfire.transform.parent = cell.transform;
        campfire.transform.localPosition = new Vector3(0f, -0.04f, 0f);
        campfireCount++;
    }
}
