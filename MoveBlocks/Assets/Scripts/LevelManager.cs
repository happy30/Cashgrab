using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelClass[] levels;
    public int currentLevel;
    public int progress;

    public GameObject completed;
    public GameObject reset;

    public bool isCompleted;
    public bool addExit;
    public Text progressText;

    public Transform[] buttons;
    public GameObject particleObject;
    public GameObject spawnedParticleObject;

    public TileManager tileManager;

    void Awake()
    {
        tileManager = Camera.main.GetComponent<TileManager>();
    }


    void Update()
    {
        progressText.text = (currentLevel + 1).ToString();
        if(progress == levels[currentLevel].targets)
        {
            //completed.SetActive(true);
            //reset.SetActive(false);
            //isCompleted = true;
            addExit = true;
            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                if (tileManager.tiles[i].tileType == TileClass.TileType.Stairs)
                {
                    tileManager.tiles[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            reset.SetActive(true);
            addExit = false;
        }

        if(isCompleted)
        {
            completed.SetActive(true);
            reset.SetActive(false);
        }
    }

    public void ResetLevel()
    {
        GameObject[] delete = GameObject.FindGameObjectsWithTag("Tile");
        for(int i = 0; i < delete.Length; i++)
        {
            Destroy(delete[i]);
        }

        Camera.main.GetComponent<TileManager>().tiles.Clear();
        Camera.main.GetComponent<GenerateLevel>().CreateLevel(levels[currentLevel].levelTexture);
        Camera.main.GetComponent<GenerateLevel>().AddCratesAndPlayer(levels[currentLevel].levelTexture);
    }

    public void CompleteLevel()
    {
        currentLevel++;
        ResetLevel();
        completed.SetActive(false);
        isCompleted = false;
        progress = 0;
    }

    public void Move(int dir)
    {
        spawnedParticleObject = Instantiate(particleObject, buttons[dir].position, Quaternion.identity) as GameObject;
        Destroy(spawnedParticleObject, 2);
        PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        if(!player.isSliding)
        {
            if (dir == 0)
            {
                player.MoveUp();
            }
            if (dir == 1)
            {
                player.MoveRight();
            }
            if (dir == 2)
            {
                player.MoveDown();
            }
            if (dir == 3)
            {
                player.MoveLeft();
            }
        }
        
    }

	
}
