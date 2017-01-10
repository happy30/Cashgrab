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
    public GameObject fakeCrate;
    public GameObject marble;

    public GameObject player;
    public GameObject player2;
    public GameObject crate;
    public bool spawnFemale;

    public Transform marblePlace;
    public GameObject uiMarble;
    GameObject spawnedUIMarble;

    GameObject spawnedTile;
    public TileManager tileManager;
    public LevelManager levelManager;
    public StatsManager stats;
    public SpriteLayerController sprites;

    public Animator an;
    public Animator anf;

    public Sprite spr;
    public Sprite sprf;

    public Sprite greyCrate;
    public Sprite greyTarget;

    public int colourCounter;
    public int colourTargetCounter;

    void Awake()
    {
        colourCounter = 0;
        colourTargetCounter = 0;
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        sprites = GameObject.Find("GameManager").GetComponent<SpriteLayerController>();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        tileManager = Camera.main.GetComponent<TileManager>();
        levelManager.currentLevel = GameObject.Find("Stats").GetComponent<StatsManager>().levelToLoad;
        CreateLevel(levelManager.levels[levelManager.currentLevel].levelTexture);
        AddCratesAndPlayer(levelManager.levels[levelManager.currentLevel].levelTexture);
        SetMarbles(levelManager.levels[levelManager.currentLevel]);
        levelManager.playersOnExit = 0;
        sprites.AssignPlayers();
    }

    public void CreateLevel (Texture2D level)
    {
        colourCounter = 0;
        colourTargetCounter = 0;
        spawnFemale = false;
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
                else if (level.GetPixel(x, y).r > 0.6f && level.GetPixel(x, y).r < 0.7f && level.GetPixel(x, y).g == 0 || level.GetPixel(x, y).r > 0.6f && level.GetPixel(x, y).r < 0.7f && level.GetPixel(x, y).g == 1)
                {
                    spawnedTile = (GameObject)Instantiate(floor, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                else if (level.GetPixel(x, y).r == 1)
                {
                    spawnedTile = (GameObject)Instantiate(target, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                    if (levelManager.levels[levelManager.currentLevel].colouredSeed == 1)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = greyTarget;
                        if (colourTargetCounter == 0)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.yellow;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 1)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 2)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.red;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 3)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.magenta;
                            colourTargetCounter++;
                        }
                    }
                    else if (levelManager.levels[levelManager.currentLevel].colouredSeed == 2)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = greyTarget;
                        if (colourTargetCounter == 0)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 1)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.yellow;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 2)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.magenta;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 3)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.red;
                            colourTargetCounter++;
                        }
                    }
                    else if (levelManager.levels[levelManager.currentLevel].colouredSeed == 3)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = greyTarget;
                        if (colourTargetCounter == 0)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 1)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.red;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 2)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.yellow;
                            colourTargetCounter++;
                        }
                        else if (colourTargetCounter == 3)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.magenta;
                            colourTargetCounter++;
                        }
                    }
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
                    if(!spawnFemale)
                    {
                        spawnedTile = (GameObject)Instantiate(player, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                        if (stats.male)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().sprite = spr;
                            spawnedTile.GetComponent<Animator>().runtimeAnimatorController = an.runtimeAnimatorController;
                        }
                        else
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().sprite = sprf;
                            spawnedTile.GetComponent<Animator>().runtimeAnimatorController = anf.runtimeAnimatorController;
                        }
                        spawnFemale = true;
                    }
                    else
                    {
                        spawnedTile = (GameObject)Instantiate(player2, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                        if (stats.male)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().sprite = sprf;
                            spawnedTile.GetComponent<Animator>().runtimeAnimatorController = anf.runtimeAnimatorController;
                        }
                        else
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().sprite = spr;
                            spawnedTile.GetComponent<Animator>().runtimeAnimatorController = an.runtimeAnimatorController;
                        }
                    }
                    
                }
                else if (level.GetPixel(x, y).b == 1 && level.GetPixel(x, y).g != 1)
                {
                    spawnedTile = (GameObject)Instantiate(crate, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                    if(levelManager.levels[levelManager.currentLevel].colouredSeed > 0)
                    {
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = greyCrate;
                        if (colourCounter == 0)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.yellow;
                            colourCounter++;
                        }
                        else if (colourCounter == 1)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                            colourCounter++;
                        }
                        else if (colourCounter == 2)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.red;
                            colourCounter++;
                        }
                        else if (colourCounter == 3)
                        {
                            spawnedTile.GetComponent<SpriteRenderer>().color = Color.magenta;
                            colourCounter++;
                        }
                    }
                }
                else if (level.GetPixel(x, y).g == 1 && level.GetPixel(x, y).b != 1)
                {
                    spawnedTile = (GameObject)Instantiate(stairs, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                    spawnedTile.SetActive(false);
                }
                else if(level.GetPixel(x, y).g == 1 && level.GetPixel(x, y).b == 1)
                {
                    spawnedTile = (GameObject)Instantiate(fakeCrate, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                    spawnedTile.GetComponent<TileClass>().fake = true;
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

    public void SetMarbles(LevelClass level)
    {
        if(level.marbles > 0)
        {
            for(int i = 0; i < level.marbles; i++)
            {
                spawnedUIMarble = (GameObject)Instantiate(uiMarble);
                spawnedUIMarble.transform.SetParent(marblePlace);
                spawnedUIMarble.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                spawnedUIMarble.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 64, 0);
                levelManager.UIMarbles.Add(spawnedUIMarble);
            }
        }
    }

    public void CreateSecondaryLayer(Texture2D layer)
    {
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 16; y++)
            {
                if (layer.GetPixel(x, y).b == 1)
                {
                    spawnedTile = (GameObject)Instantiate(marble, new Vector3(x * 0.64f, y * 0.64f, 0), Quaternion.identity);
                }
                if (spawnedTile != null)
                {
                    spawnedTile.GetComponent<TileClass>().loc = new Vector2(x * 0.64f, y * 0.64f);
                    tileManager.tiles2.Add(spawnedTile.GetComponent<TileClass>());
                }
                spawnedTile = null;
            }
        }
    }
}
