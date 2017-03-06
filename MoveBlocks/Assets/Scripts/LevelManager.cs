using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelClass[] levels;
    public int currentLevel;
    public int progress;
    public int marbleProgress;

    public GameObject completed;
    public GameObject reset;
    public GameObject undo;

    public bool isCompleted;
    public bool addExit;
    public Text progressText;
    public Text keysText;
    public Text completedText;

    public int adCheat;

    public GameObject extraKey;

    public GameObject keySprite;
    public GameObject keyParticle;
    GameObject spawnedKeyParticle;

    public Transform[] buttons;
    public GameObject particleObject;
    public GameObject spawnedParticleObject;
    public RectTransform completedButton;

    public SpriteLayerController sprites;

    public StatsManager stats;

    public TileManager tileManager;

    public int block;
    public int level;

    public bool holding;
    public int dirTo;
    public float timer;

    public bool holdingf;
    public int dirTof;
    public float timerf;

    public AudioSource _sound;
    public AudioClip move;
    public AudioClip push;
    public AudioClip resetSound;
    public AudioClip openExit;
    public AudioClip completeLevel;
    public AudioClip onTarget;
    public AudioClip blocked;
    public AudioClip button;
    public AudioClip openMenu;
    public AudioClip closeMenu;

    public List<GameObject> UIMarbles = new List<GameObject>();

    public int playersOnExit;

    public bool coop;
    public bool marblesSpawned;

    public bool fading;
    public AudioSource fastSound;
    public AudioSource slowSound;
    public OptionsSettings options;

    public bool playerExitOpenSound;

    void Awake()
    {
        options = GameObject.Find("Stats").GetComponent<OptionsSettings>();
        tileManager = Camera.main.GetComponent<TileManager>();
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        _sound = GetComponent<AudioSource>();
        sprites = GetComponent<SpriteLayerController>();
        if(!coop)
        {
            keysText.text = ": " + stats.keys;
        }
        slowSound.volume = options.BGMFactor;
        
    }

    void Update()
    {
        int block = (currentLevel / 10) + 1;
        int level = currentLevel - ((block - 1) * 10) + 1;


        if(addExit)
        {
            undo.SetActive(false);
        }

        progressText.text = (block + "." + level);
        if(progress >= levels[currentLevel].targets && marbleProgress == levels[currentLevel].marbles)
        {
            //completed.SetActive(true);
            //reset.SetActive(false);
            //isCompleted = true;
            addExit = true;
            if(!playerExitOpenSound)
            {
                _sound.PlayOneShot(openExit, options.SFXFactor);
                playerExitOpenSound = true;
            }
            
            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                if (tileManager.tiles[i].tileType == TileClass.TileType.Stairs)
                {
                    tileManager.tiles[i].gameObject.SetActive(true);
                }
            }
        }
        else if (progress == levels[currentLevel].targets && !marblesSpawned)
        {
            addExit = true;
            Camera.main.GetComponent<GenerateLevel>().CreateSecondaryLayer(levels[currentLevel].secondaryLevelTexture);
            marblesSpawned = true;
        }
        else if ((progress != levels[currentLevel].targets && !marblesSpawned))
        {
            reset.SetActive(true);
            addExit = false;
        }

        if (isCompleted)
        {
            fading = true;
            completed.SetActive(true);
            reset.SetActive(false);
            if (!stats.clearedLevels[currentLevel] && !coop)
            {
                extraKey.SetActive(true);
                completedText.text = "First Clear!";
                completedButton.sizeDelta = new Vector2(completedButton.sizeDelta.x, 300);
                if (!stats.unlockedLevels[currentLevel + 1])
                {
                    completedText.text = "First Clear! \n Unlock more levels to continue...";
                    completedButton.sizeDelta = new Vector2(completedButton.sizeDelta.x, 500);
                }
            }
            else if (!stats.unlockedLevels[currentLevel + 1] && !coop)
            {
                completedButton.sizeDelta = new Vector2(completedButton.sizeDelta.x, 350);
                completedText.text = "Unlock more\nlevels to\ncontinue!";
            }
            else if(!coop)
            {
                completedButton.sizeDelta = new Vector2(completedButton.sizeDelta.x, 200);
                completedText.text = "";
            }

        }

        //Player 1
        if(holding)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer > 0.3f)
        {
            PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
            if (dirTo == 0)
            {
                player.MoveUp();
            }
            if (dirTo == 1)
            {
                player.MoveRight();
            }
            if (dirTo == 2)
            {
                player.MoveDown();
            }
            if (dirTo == 3)
            {
                player.MoveLeft();
            }
        }

        //Player 2
        if (holdingf)
        {
            timerf += Time.deltaTime;
        }
        else
        {
            timerf = 0;
        }

        if (timerf > 0.3f)
        {
            PlayerController playerf = GameObject.Find("Playerf(Clone)").GetComponent<PlayerController>();
            if (dirTof == 4)
            {
                playerf.MoveUp();
            }
            if (dirTof == 5)
            {
                playerf.MoveRight();
            }
            if (dirTof == 6)
            {
                playerf.MoveDown();
            }
            if (dirTof == 7)
            {
                playerf.MoveLeft();
            }
        }

        if (fading)
        {
            if(fastSound.volume < 0.8f * options.BGMFactor)
            {
                fastSound.volume += Time.deltaTime;
            }
            else
            {
                fading = false;
            }
            
        }
    }

    public void ResetLevel()
    {
        if(!coop)
        {
            if (Camera.main.GetComponent<AdManager>().interstitial.IsLoaded() && !stats.disableAds)
            {
                Debug.Log("play ad...");
                Camera.main.GetComponent<AdManager>().interstitial.Show();
            }
        }
        playersOnExit = 0;


        marblesSpawned = false;
        marbleProgress = 0;
        progress = 0;
        fastSound.volume = 0;
        fading = false;
        _sound.PlayOneShot(resetSound, options.SFXFactor);
        GameObject[] delete = GameObject.FindGameObjectsWithTag("Tile");
        for(int i = 0; i < delete.Length; i++)
        {
            Destroy(delete[i]);
        }

        Camera.main.GetComponent<TileManager>().tiles.Clear();
        Camera.main.GetComponent<TileManager>().tiles2.Clear();
        UIMarbles.Clear();
        Camera.main.GetComponent<GenerateLevel>().CreateLevel(levels[currentLevel].levelTexture);
        Camera.main.GetComponent<GenerateLevel>().AddCratesAndPlayer(levels[currentLevel].levelTexture);
        Camera.main.GetComponent<GenerateLevel>().SetMarbles(levels[currentLevel]);
        sprites.AssignPlayers();
        reset.SetActive(true);
        addExit = false;
        undo.SetActive(false);
    }

    public void CompleteLevel()
    {
        if(!coop)
        {
            playerExitOpenSound = false;

            if(!stats.clearedLevels[currentLevel])
            {
                stats.ClearLevel(currentLevel, false);
                Destroy(spawnedKeyParticle = (GameObject)Instantiate(keyParticle, keySprite.transform.position, Quaternion.identity), 2f);
                spawnedKeyParticle.transform.SetParent(keySprite.transform);
                spawnedKeyParticle.transform.localScale = new Vector3(1, 1, 1);
                keysText.text = ": " + stats.keys;
            }
            extraKey.SetActive(false);
            currentLevel++;
            if (!stats.unlockedLevels[currentLevel])
            {
                completed.SetActive(false);
                isCompleted = false;
                progress = 0;
                Camera.main.GetComponent<AdManager>().bannerView.Hide();
                SceneManager.LoadScene(3);
            }
            else
            {
                ResetLevel();
                completed.SetActive(false);
                isCompleted = false;
                progress = 0;
            }
            
        }
        else
        {
            playerExitOpenSound = false;
            stats.ClearLevel(currentLevel, true);
            currentLevel++;
            if (currentLevel > 9)
            {
                currentLevel = 0;
            }
            ResetLevel();
            completed.SetActive(false);
            isCompleted = false;
            progress = 0;
        }
        
    }

    public void Move(int dir)
    {
        if(coop && dir > 3)
        {
            dirTof = dir;
            spawnedParticleObject = Instantiate(particleObject, buttons[dir].position, Quaternion.identity) as GameObject;
            Destroy(spawnedParticleObject, 2);
            PlayerController playerf = GameObject.Find("Playerf(Clone)").GetComponent<PlayerController>();
            if (!playerf.isSliding)
            {
                if (dir == 4)
                {
                    playerf.MoveUp();
                }
                if (dir == 5)
                {
                    playerf.MoveRight();
                }
                if (dir == 6)
                {
                    playerf.MoveDown();
                }
                if (dir == 7)
                {
                    playerf.MoveLeft();
                }
            }
            holdingf = true;
        }
        else
        {
            PlayerController player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
            dirTo = dir;
            spawnedParticleObject = Instantiate(particleObject, buttons[dir].position, Quaternion.identity) as GameObject;
            Destroy(spawnedParticleObject, 2);
            if (!player.isSliding)
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
            holding = true;
        }
    }

    public void NoTouch(bool secondPlayer)
    {
        if(secondPlayer)
        {
            holdingf = false;
            timerf = 0;
        }
        else
        {
            holding = false;
            timer = 0;
        }
    }

    public void PlayerOnExit(PlayerController player)
    {
        playersOnExit++;
        if(coop)
        {
            if(playersOnExit == 2)
            {
                isCompleted = true;
                playersOnExit = 0;
            }
            else
            {
                player.completedCoop = true;
            }
             
        }
        else
        {
            isCompleted = true;
        }
    }
}
