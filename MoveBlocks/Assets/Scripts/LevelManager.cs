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
    public Text progressText;


    void Update()
    {
        progressText.text = "Level: " + (currentLevel + 1).ToString();
        if(currentLevel == 4)
        {
            progressText.text += " BOSS!!!";
        }
        if(progress == levels[currentLevel].targets)
        {
            completed.SetActive(true);
            reset.SetActive(false);
            isCompleted = true;
        }
        else
        {
            reset.SetActive(true);
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
        PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        if(dir == 0)
        {
            player.MoveUp();
        }
        if(dir == 1)
        {
            player.MoveRight();
        }
        if(dir==2)
        {
            player.MoveDown();
        }
        if(dir==3)
        {
            player.MoveLeft();
        }
    }

	
}
