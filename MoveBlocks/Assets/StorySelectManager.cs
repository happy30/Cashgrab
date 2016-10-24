using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StorySelectManager : MonoBehaviour
{
    public bool inCutscene;
    public bool moveRight;

    public float yPos;

    public float moveSpeed;

    public RectTransform player;

    public Text totalKeys;
    public StatsManager stats;

    public int[] keysNeeded;

    public GameObject[] lockedDoors;
    public RectTransform overlay;
    public float overlayYDest;
    public float overlayTime;

    public GameObject doorSpeech;

    public GameObject menuButton;
    public GameObject playButton;

    void Awake()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        overlayYDest = -360 - (stats.unlockedDoors * 280);
        overlay.anchoredPosition = new Vector2(overlay.anchoredPosition.x, overlayYDest);

        if (stats.unlockedDoors % 2 == 1)
        {
            moveRight = true;
        }

        for(int i = 0; i < stats.unlockedDoors; i++)
        {
            Destroy(lockedDoors[i]);
        }

        player.anchoredPosition = stats.playerPos;
        doorSpeech.GetComponent<RectTransform>().anchoredPosition = new Vector2(lockedDoors[stats.unlockedDoors].GetComponent<RectTransform>().anchoredPosition.x + 120, lockedDoors[stats.unlockedDoors].GetComponent<RectTransform>().anchoredPosition.y + 120);
    }


    void Update()
    {
        if(doorSpeech.activeSelf)
        {
            doorSpeech.GetComponentInChildren<Text>().text = stats.keys + "/" + keysNeeded[stats.unlockedDoors];
        }
        
        totalKeys.text = ": " + stats.keys.ToString();
        overlay.anchoredPosition = Vector2.Lerp(overlay.anchoredPosition, new Vector2(overlay.anchoredPosition.x, overlayYDest), overlayTime * Time.deltaTime);

        if(inCutscene)
        {
            menuButton.SetActive(false);
            playButton.SetActive(false);
            doorSpeech.SetActive(false);

            if(moveRight)
            {
                
                if(player.anchoredPosition.x < 380)
                {
                    player.anchoredPosition += new Vector2(moveSpeed * Time.deltaTime, 0);
                    yPos = player.anchoredPosition.y;
                }
                else
                {
                    
                    if(yPos - player.anchoredPosition.y < 270)
                    {
                        player.anchoredPosition += new Vector2(0, -moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        stats.playerPos = player.anchoredPosition;
                        PlayerPrefsX.SetVector2("PlayerPos", player.anchoredPosition);
                        inCutscene = false;
                    }
                }
            }
            else
            {
                if (player.anchoredPosition.x > -330)
                {
                    player.anchoredPosition += new Vector2(-moveSpeed * Time.deltaTime, 0);
                    yPos = player.anchoredPosition.y;
                }
                else
                {
                    if (yPos - player.anchoredPosition.y < 270)
                    {
                        player.anchoredPosition += new Vector2(0, -moveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        stats.playerPos = player.anchoredPosition;
                        PlayerPrefsX.SetVector2("PlayerPos", player.anchoredPosition);
                        inCutscene = false;
                    }
                }
            }

        }
        else
        {
            menuButton.SetActive(true);
            playButton.SetActive(true);
            doorSpeech.SetActive(true);
        }

    }

    public void UnlockDoor()
    {
        if(stats.keys >= keysNeeded[stats.unlockedDoors] && !inCutscene)
        {
            Destroy(lockedDoors[stats.unlockedDoors]);
            stats.unlockedDoors++;
            overlayYDest -= 280;
            doorSpeech.GetComponent<RectTransform>().anchoredPosition = new Vector2(lockedDoors[stats.unlockedDoors].GetComponent<RectTransform>().anchoredPosition.x + 120, lockedDoors[stats.unlockedDoors].GetComponent<RectTransform>().anchoredPosition.y +120);

            inCutscene = true;
            if(!moveRight)
            {
                moveRight = true;
            }
            else
            {
                moveRight = false;
            }
            PlayerPrefs.SetInt("UnlockedDoors", stats.unlockedDoors);
        }
    }

}
