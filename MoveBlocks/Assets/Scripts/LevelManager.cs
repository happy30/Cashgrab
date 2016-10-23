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

    public int playersOnExit;

    public bool coop;

    public bool fading;
    public AudioSource fastSound;

    public bool playerExitOpenSound;

    void Awake()
    {
        tileManager = Camera.main.GetComponent<TileManager>();
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
        _sound = GetComponent<AudioSource>();
        sprites = GetComponent<SpriteLayerController>();
    }

    void Update()
    {
        int block = (currentLevel / 10) + 1;
        int level = currentLevel - ((block - 1) * 10) + 1;

        progressText.text = (block + "." + level);
        if(progress == levels[currentLevel].targets)
        {
            //completed.SetActive(true);
            //reset.SetActive(false);
            //isCompleted = true;
            addExit = true;
            if(!playerExitOpenSound)
            {
                _sound.PlayOneShot(openExit, 1f);
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
        else
        {
            reset.SetActive(true);
            addExit = false;
        }

        if(isCompleted)
        {
            fading = true;
            completed.SetActive(true);
            reset.SetActive(false);
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
            if(fastSound.volume < 0.8f)
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
        progress = 0;
        fastSound.volume = 0;
        fading = false;
        _sound.PlayOneShot(resetSound);
        GameObject[] delete = GameObject.FindGameObjectsWithTag("Tile");
        for(int i = 0; i < delete.Length; i++)
        {
            Destroy(delete[i]);
        }

        Camera.main.GetComponent<TileManager>().tiles.Clear();
        Camera.main.GetComponent<GenerateLevel>().CreateLevel(levels[currentLevel].levelTexture);
        Camera.main.GetComponent<GenerateLevel>().AddCratesAndPlayer(levels[currentLevel].levelTexture);
        sprites.AssignPlayers();
        reset.SetActive(true);
    }

    public void CompleteLevel()
    {
        if(!coop)
        {
            playerExitOpenSound = false;
            stats.ClearLevel(currentLevel, false);
            currentLevel++;
            if (currentLevel > 29)
            {
                currentLevel = 0;
            }
            ResetLevel();
            completed.SetActive(false);
            isCompleted = false;
            progress = 0;
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
