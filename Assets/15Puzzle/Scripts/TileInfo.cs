using UnityEngine;
using TMPro;

public class TileInfo : MonoBehaviour
{
    public int id;
    public int[] coords = new int[2];
    public Puzzle15 root;

    public void setRoot(Puzzle15 root)
    {
        this.root = root;
    }
    public void setCoords(int x, int y)
    {
        coords[0] = x;
        coords[1] = y;
        gameObject.transform.position = new Vector2(root.topleftEdge.x + Puzzle15.tileX * (x + 0.5f), root.topleftEdge.y- Puzzle15.tileY * (y + 0.5f));
    }
    public void SetID(int id)
    {
        this.id = id;
        GetComponentInChildren<TextMeshPro>().text = id.ToString();
    }

    public bool InCorrectPosition()
    {
        int currentPosition = coords[0] + coords[1] * Puzzle15.xSize + 1;
        return (id == currentPosition);
    }

    private void OnMouseDown()
    {
        int distance = Mathf.Abs(coords[0] - root.emptyCoords[0]) + Mathf.Abs(coords[1] - root.emptyCoords[1]);
        if (distance == 1)
        {
            // check that this is a copy, not a link
            int[] temp = new int[2] { coords[0], coords[1] };
            this.setCoords(root.emptyCoords[0], root.emptyCoords[1]);
            root.SetEmptyCoords(temp);
            root.CheckSolved();
        }
    }

}
