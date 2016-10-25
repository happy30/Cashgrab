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

    public GameObject settingsPanel;
    public GameObject surePanel;
    public Text surePanelText;

    GameObject spawnedButton;

    public int yRow;
    public int yPos;
    public int xPos;

    public List<GameObject> buttons = new List<GameObject>();

    AudioSource _sound;
    public AudioSource fastSound;
    public AudioClip button;
    public AudioClip closeMenu;
    public AudioClip resetSound;
    public bool coopSelect;

    public bool fading;
    public bool mainMenu;


    public void Start()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        _sound = GetComponent<AudioSource>();
        if(mainMenu)
        {
            SelectGender(stats.male);
        }
        
    }

    void Update()
    {
        if(fading)
        {
            if(fastSound.volume < 1)
            {
                fastSound.volume += Time.deltaTime;
            }
            else
            {
                fading = false;
            }
        }
    }

    public void LevelSelect(bool coop)
    {
        if(!coop)
        {
            coopSelect = false;
            fading = true;
            _sound.PlayOneShot(button);
            levelSelectPanel.SetActive(true);
            for (int x = 0; x < stats.totalLevels; x++)
            {
                if (stats.unlockedLevels[x])
                {
                    spawnedButton = (GameObject)Instantiate(unlockedButton);
                    int block = (x / 10) + 1;
                    int level = x - ((block - 1) * 10) + 1;
                    spawnedButton.GetComponentInChildren<Text>().text = block + "." + level.ToString();
                    if (stats.clearedLevels[x])
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
                if (xPos > 800)
                {
                    xPos = 0;
                }
                yRow++;
                if (yRow == 5)
                {
                    yPos -= 200;
                }
                if (yRow > 9)
                {
                    yPos -= 400;
                    yRow = 0;
                }
                buttons.Add(spawnedButton);
            }

        }
        else
        {
            coopSelect = true;
            fading = true;
            _sound.PlayOneShot(button);
            levelSelectPanel.SetActive(true);
            for (int x = 0; x < stats.totalLevelsCoop; x++)
            {
                if (stats.unlockedLevelsCoop[x])
                {
                    spawnedButton = (GameObject)Instantiate(unlockedButton);
                    int block = (x / 10) + 1;
                    int level = x - ((block - 1) * 10) + 1;
                    spawnedButton.GetComponentInChildren<Text>().text = block + "." + level.ToString();
                    if (stats.clearedLevelsCoop[x])
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
                if (xPos > 800)
                {
                    xPos = 0;
                }
                yRow++;
                if (yRow == 5)
                {
                    yPos -= 200;
                }
                if (yRow > 9)
                {
                    yPos -= 300;
                    yRow = 0;
                }
                buttons.Add(spawnedButton);
            }
        }
        
    }

    public void CloseLevelSelect()
    {
        fastSound.volume = 0f;
        fading = false;
        _sound.PlayOneShot(closeMenu);
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
        _sound.PlayOneShot(button);
        stats.levelToLoad = lv;
        if(!coopSelect)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    public void SelectGender(bool isMale)
    {
        if(isMale != stats.male)
        {
            _sound.PlayOneShot(button);
        }
        stats.male = isMale;
        if (stats.male == true)
        {
            GameObject.Find("PlayerM").GetComponent<Outline>().enabled = true;
            GameObject.Find("PlayerF").GetComponent<Outline>().enabled = false;
        }
        else
        {
            GameObject.Find("PlayerM").GetComponent<Outline>().enabled = false;
            GameObject.Find("PlayerF").GetComponent<Outline>().enabled = true;
        }
    }

    public void OpenSettings()
    {
        _sound.PlayOneShot(button);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        surePanelText.text = "Are you sure?";
        _sound.PlayOneShot(closeMenu);
        settingsPanel.SetActive(false);
        surePanel.SetActive(false);
    }

    public void ClearData()
    {
        _sound.PlayOneShot(button);
        surePanel.SetActive(true);
    }
    public void SureYes()
    {
        surePanelText.text = "All data has been reset!";
        _sound.PlayOneShot(resetSound);
        stats.ClearData();
    }
    public void SureNo()
    {
        _sound.PlayOneShot(button);
        surePanel.SetActive(false);
    }

    public void ToMainMenu()
    {
        _sound.PlayOneShot(button);
        SceneManager.LoadScene(0);
    }

    public void ToStorySelect()
    {
        _sound.PlayOneShot(button);
        SceneManager.LoadScene(3);
    }
}
