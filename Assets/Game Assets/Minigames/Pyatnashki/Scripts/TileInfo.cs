using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour
{
    public int id;
    public int pointX, pointY;
    public Puzzle15 root;

    [SerializeField] private float moveTime = 0.25f;
    [SerializeField] private float finalizeTime = 0.25f;
    public bool inPlay = true;

    #region SET METHODS
    public void setRoot(Puzzle15 root)
    {
        this.root = root;
    }
    public void setCoords(int x, int y)
    {
        pointX = x;
        pointY = y;
        gameObject.transform.position = PointsToCoords(x, y);
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    #endregion

    #region CHECK METHODS
    public bool InCorrectPosition()
    {
        int currentPosition = pointX + pointY * root.xSize + 1;
        return (id == currentPosition);
    }
    #endregion

    #region INPUT RESPONSE

    private void OnMouseDown()
    {
        if (!inPlay) return;

        int distance = Mathf.Abs(pointX - root.emptyX) + Mathf.Abs(pointY - root.emptyY);
        if (distance == 1)
        {
            int tempX = pointX, tempY = pointY;
            this.moveCoords(tempX, tempY, root.emptyX, root.emptyY, moveTime);
            root.SetEmptyCoords(tempX, tempY);
            root.CheckSolved();
        }
    }
    #endregion

    #region Move Methods
    private void moveCoords(int originX, int originY, int destX, int destY, float time)
    {
        pointX = destX;
        pointY = destY;
        MoveTile(PointsToCoords(originX, originY), PointsToCoords(destX, destY), time);

    }
    #endregion

    #region General Methods
    private Vector2 PointsToCoords(int x, int y)
    {
        return new Vector2(root.topleftEdge.x + root.tileX * (x + 0.5f), root.topleftEdge.y - root.tileY * (y + 0.5f));
    }

    private void MoveTile(Vector2 original, Vector2 destination, float time)
    {
        StartCoroutine(MoveRoutine(original, destination, time));
    }

    public void AppearTile(float time) {
        StartCoroutine(AppearRoutine(time));
    }

    public void FinishGame()
    {
        inPlay = false;
        GetComponentInChildren<TileBorder>().Dissapear(finalizeTime);
    }
    #endregion

    #region COROUTINES
    private IEnumerator MoveRoutine(Vector2 original, Vector2 destination, float time) {
        float progress = 0;
        do
        {
            progress += Time.deltaTime;
            float xPos = Mathf.Lerp(original.x, destination.x, progress / time);
            float yPos = Mathf.Lerp(original.y, destination.y, progress / time);
            gameObject.transform.position = new Vector2(xPos, yPos);
            yield return null;
        } while (progress < time);
    }

    private IEnumerator AppearRoutine(float time) {
        SpriteRenderer image = gameObject.GetComponent<SpriteRenderer>();
        image.color = new Color(1f, 1f, 1f, 0f);
        float progress = 0;
        while (progress < time) {
            progress += Time.deltaTime;
            image.color = new Color(1f, 1f, 1f, progress / time);
            yield return null;
        }
    }
    #endregion

}
