using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour
{
    public GameObject empty;
    public GameObject wall;
    public GameObject floor;
    public GameObject target;
    public GameObject stairs;
    public GameObject ice;

    public GameObject player;
    public GameObject crate;

    GameObject spawnedTile;
    public TileManager tileManager;
    public LevelManager levelManager;

    void Awake()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        tileManager = Camera.main.GetComponent<TileManager>();
        CreateLevel(levelManager.levels[levelManager.currentLevel].levelTexture);
        AddCratesAndPlayer(levelManager.levels[levelManager.currentLevel].levelTexture);
    }
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void CreateLevel (Texture2D level)
    {
        levelManager.isCompleted = false;
        for(int x = 0; x < 16; x++)
        {
            for(int y = 0; y <16; y++)
            {
                if(level.GetPixel(x,y).r == 0)
                {
                    spawnedTile = (GameObject)Instantiate(empty, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).r > 0.3f && level.GetPixel(x, y).r < 0.4f)
                {
                    spawnedTile = (GameObject)Instantiate(wall, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).r > 0.6f && level.GetPixel(x, y).r < 0.7f && level.GetPixel(x, y).g == 0)
                {
                    spawnedTile = (GameObject)Instantiate(floor, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).r == 1)
                {
                    spawnedTile = (GameObject)Instantiate(target, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).g > 0.3f && level.GetPixel(x, y).g < 0.7f)
                {
                    spawnedTile = (GameObject)Instantiate(ice, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                if (spawnedTile != null)
                {
                    spawnedTile.GetComponent<TileClass>().loc = new Vector2(x * 0.64f, y * 0.64f);
                    tileManager.tiles.Add(spawnedTile.GetComponent<TileClass>());
                }
                spawnedTile = null;
            }
        }
    }
    public void AddCratesAndPlayer(Texture2D level)
    {
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                if (level.GetPixel(x, y).b > 0.2f && level.GetPixel(x, y).b < 0.6f)
                {
                    spawnedTile = (GameObject)Instantiate(player, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).b == 1)
                {
                    spawnedTile = (GameObject)Instantiate(crate, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).g == 1)
                {
                    spawnedTile = (GameObject)Instantiate(stairs, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                    spawnedTile.SetActive(false);
                }
                if (spawnedTile != null)
                {
                    spawnedTile.GetComponent<TileClass>().loc = spawnedTile.transform.position;
                    tileManager.tiles.Add(spawnedTile.GetComponent<TileClass>());
                }
                spawnedTile = null;
            }
            
        }
        
    }
}
