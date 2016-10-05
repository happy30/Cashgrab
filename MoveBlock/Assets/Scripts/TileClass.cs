using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour
{
    public enum TileType
    {
        Floor,
        Wall,
        Crate,
        Target
    };

    public int pos;
    public TileType tileType;

}
