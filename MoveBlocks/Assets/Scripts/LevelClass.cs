using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelClass
{
    public Texture2D levelTexture;
    public int targets;
    
    public bool hasSecondaryMap;
    public Texture2D secondaryLevelTexture;
    public int marbles;

    public int colouredSeed;
}
