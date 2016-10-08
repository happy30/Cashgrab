using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour
{
    public enum TileType
    {
        Wall,
        Floor,
        Target,
        Crate,
        Player
    };

    public TileType tileType;
    public Vector2 loc;
    public bool onTarget;

    public void UpdatePos(Vector2 position)
    {
        transform.position = position;
        loc = position;
    }
}
