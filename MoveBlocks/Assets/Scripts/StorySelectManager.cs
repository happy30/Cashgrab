using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StorySelectManager : MonoBehaviour
{
    public bool inCutscene;
    public bool moveRight;

    public float yPos;

    public float moveSpeed;

    public GameObject sweetPanel;

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

    public AudioClip unlock;
    public AudioClip button;
    public AudioClip back;
    public AudioClip block;

    public AudioClip bgm;
    public AudioClip walkbgm;

    public AudioSource bgmsound;

    AudioSource _sound;

    void Awake()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        overlayYDest = -360 - (stats.unlockedDoors * 280);
        overlay.anchoredPosition = new Vector2(overlay.anchoredPosition.x, overlayYDest);
        bgmsound.clip = bgm;
        _sound = GetComponent<AudioSource>();

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
            if(bgmsound.clip != walkbgm)
            {
                bgmsound.clip = walkbgm;
                bgmsound.Play();
            }

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
                        sweetPanel.SetActive(true);
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
                        sweetPanel.SetActive(true);
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
            _sound.PlayOneShot(unlock);
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

            for(int i = 0; i < stats.unlockedLevels.Length; i++)
            {
                if(!stats.unlockedLevels[i])
                {
                    for(int x = 0; x < 10; x++)
                    {
                        stats.unlockedLevels[i + x] = true;
                        
                    }
                    PlayerPrefsX.SetBoolArray("UnlockedLevels", stats.unlockedLevels);
                    break;
                }
            }
        }
        else
        {
            _sound.PlayOneShot(block);
        }
    }

    public void SweetButton()
    {
        sweetPanel.SetActive(false);
        inCutscene = false;
        _sound.PlayOneShot(button);
        if (bgmsound.clip != bgm)
        {
            bgmsound.clip = bgm;
            bgmsound.Play();
        }
    }

}
