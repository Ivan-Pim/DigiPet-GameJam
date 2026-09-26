using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Puzzle15 : MonoBehaviour
{
    public const int xSize = 4, ySize = 4, tileX = 5, tileY = 5;
    public Vector2 topleftEdge = new Vector2(-10f, 10f);
    [SerializeField] GameObject tilePrefab;

    public TileInfo[] thePuzzle = new TileInfo[xSize * ySize - 1];
    public int[] numArray = new int[xSize * ySize - 1];
    public int[] emptyCoords;

    public void SetEmptyCoords(int[] coords)
    {
        emptyCoords[0] = coords[0];
        emptyCoords[1] = coords[1];
    }

    void Awake()
    {
        RandomizeTiles();
        for (int i = 0; i < xSize * ySize - 1; ++i)
        {
            int x = i % 4;
            int y = i / 4;
            thePuzzle[i] = CreateTile(x, y, numArray[i]);
        }
        SetEmptyCoords(new int[2]{xSize - 1, ySize - 1});
    }

    public TileInfo CreateTile(int x, int y, int id)
    {
        GameObject newTile = Instantiate(tilePrefab);
        newTile.transform.parent = this.transform;
        TileInfo tileinfo = newTile.GetComponentInChildren<TileInfo>();
        tileinfo.setRoot(this);
        tileinfo.SetID(id);
        tileinfo.setCoords(x, y);

        return tileinfo;
    }

    public void RandomizeTiles()
    {
        // create array from 1 to 15 as a base
        for (int i = 0; i < numArray.Length; ++i)
        {
            numArray[i] = i + 1;
        }

        // shuffle at least once, and until you get a solvable cofiguration
        do
        {
            numArray = Shuffle(numArray);
        } while (!IsSolvable(numArray));
    }
    public int[] Shuffle(int[] numArray)
    {
        // uses the Fisher-Yates Shuffle ig
        int length = numArray.Length;
        for (int i = 0; i < length - 1; ++i)
        {
            int newIndice = Random.Range(i, length);
            int temp = numArray[newIndice];
            numArray[newIndice] = numArray[i];
            numArray[i] = temp;
        }
        return numArray;
    }
    public bool IsSolvable(int[] numArray)
    {
        int res = MergeSort.inversionCount(numArray);
        return (res % 2 == 0);
    }

    public void CheckSolved()
    {
        foreach (TileInfo tile in thePuzzle)
        {
            if (!tile.InCorrectPosition())
            {
                return;
            }
        }
        Debug.Log("You Win!");
    }
}
