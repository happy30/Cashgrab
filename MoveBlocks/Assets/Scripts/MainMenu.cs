using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public StatsManager stats;
    public GameObject unlockedButton;
    public GameObject locked;

    public GameObject levelSelectPanel;
    public Transform LevelButtonPanel;

    GameObject spawnedButton;

    public int yRow;
    public int yPos;
    public int xPos;

    public List<GameObject> buttons = new List<GameObject>();


    public void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
    }

    public void LevelSelect()
    {
        levelSelectPanel.SetActive(true);
        for(int x = 0; x < stats.totalLevels; x++)
        {
            if(stats.unlockedLevels[x])
            {
                spawnedButton = (GameObject)Instantiate(unlockedButton);
                spawnedButton.GetComponentInChildren<Text>().text = "Lv " + (x + 1).ToString();
                if(stats.clearedLevels[x])
                {
                    spawnedButton.GetComponent<Image>().color = new Color(0.353f, 0.756f, 0.278f, 1);
                }
                Button tempButton = spawnedButton.GetComponent<Button>();
                int tempInt = x;
                tempButton.onClick.AddListener(() => ButtonClicked(tempInt));
            }
            else
            {
                spawnedButton = (GameObject)Instantiate(locked);
            }
            spawnedButton.transform.SetParent(LevelButtonPanel);
            spawnedButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            spawnedButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

            xPos += 200;
            if(xPos > 800)
            {
                xPos = 0;
            }
            yRow++;
            if (yRow > 4)
            {
                yPos -= 200;
                yRow = 0;
            }
            buttons.Add(spawnedButton);
        }
    }

    public void CloseLevelSelect()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
        xPos = 0;
        yPos = 0;
        levelSelectPanel.SetActive(false);
        
    }

    void ButtonClicked(int lv)
    {
        stats.levelToLoad = lv;
        SceneManager.LoadScene(1);
    }
}
