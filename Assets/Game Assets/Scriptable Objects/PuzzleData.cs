using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "Scriptable Objects/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    [Header("Image Array")]
    public Sprite[] tiles; // from top left to bottom right, include all the tiles of the completed image
}
