using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

    public int totalLevels;
    public bool[] unlockedLevels;
    public bool[] clearedLevels;

    public int totalLevelsCoop;
    public bool[] unlockedLevelsCoop;
    public bool[] clearedLevelsCoop;

    public int levelToLoad;
    public int keys;

    public static StatsManager Instance;
    public bool male;
    public int unlockedDoors;

    public Vector2 playerPos;

    void Awake()
    {
        playerPos = new Vector2(-342, 1105);
        male = true;
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }    
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    void Start()
    {

        if (PlayerPrefsX.GetBoolArray("UnlockedLevels").Length == 100)
        {
            unlockedLevels = PlayerPrefsX.GetBoolArray("UnlockedLevels");
            clearedLevels = PlayerPrefsX.GetBoolArray("ClearedLevels");
            playerPos = PlayerPrefsX.GetVector2("PlayerPos", playerPos);
            unlockedDoors = PlayerPrefs.GetInt("UnlockedDoors");
            keys = PlayerPrefs.GetInt("Keys");
        } 
        if(PlayerPrefsX.GetBoolArray("UnlockedLevelsCoop").Length > 1)
        {
            unlockedLevelsCoop = PlayerPrefsX.GetBoolArray("UnlockedLevelsCoop");
            clearedLevelsCoop = PlayerPrefsX.GetBoolArray("ClearedLevelsCoop");
        }
        
        
    }

	
    public void ClearLevel(int lv, bool coop)
    {
        if(!coop)
        {
            if(!clearedLevels[lv])
            {
                clearedLevels[lv] = true;
                keys++;
                PlayerPrefs.SetInt("Keys", keys);
            }
            
            int u = 0;
            for (int i = 0; i < clearedLevels.Length; i++)
            {
                if (clearedLevels[i])
                {
                    u++;
                }
            }
            PlayerPrefsX.SetBoolArray("ClearedLevels", clearedLevels);
        }
        else
        {
            clearedLevelsCoop[lv] = true;

            int u = 0;
            for (int i = 0; i < clearedLevelsCoop.Length; i++)
            {
                if (clearedLevelsCoop[i])
                {
                    u++;
                }
            }
            Debug.Log(u);
            Debug.Log(unlockedLevelsCoop.Length);
            if ((u + 1) < 10)
            {
                Debug.Log("cannothapen");
                unlockedLevelsCoop[u] = true;
                unlockedLevelsCoop[u + 1] = true;
            }

            PlayerPrefsX.SetBoolArray("UnlockedLevelsCoop", unlockedLevelsCoop);
            PlayerPrefsX.SetBoolArray("ClearedLevelsCoop", clearedLevelsCoop);
        }
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < unlockedLevels.Length; i ++)
        {
            if(i < 10)
            {
                unlockedLevels[i] = true;
            }
            if (i > 9)
            {
                unlockedLevels[i] = false;
            }   
        }
        for (int i = 0; i < unlockedLevelsCoop.Length; i++)
        {
            if (i > 0)
            {
                unlockedLevelsCoop[i] = false;
            }
        }
        for (int i = 0; i < clearedLevelsCoop.Length; i++)
        {
            clearedLevelsCoop[i] = false;
        }
        for (int i = 0; i < clearedLevels.Length; i++)
        {
            clearedLevels[i] = false;
        }

        keys = 0;
        unlockedDoors = 0;
        playerPos = new Vector2(-342, 1105);

        PlayerPrefsX.SetBoolArray("UnlockedLevels", unlockedLevels);
        PlayerPrefsX.SetBoolArray("ClearedLevels", clearedLevels);
        PlayerPrefsX.SetBoolArray("UnlockedLevelsCoop", unlockedLevelsCoop);
        PlayerPrefsX.SetBoolArray("ClearedLevelsCoop", clearedLevelsCoop);
        PlayerPrefs.SetInt("Keys", keys);
        PlayerPrefs.SetInt("UnlockedDoors", 0);
        PlayerPrefsX.SetVector2("PlayerPos", new Vector2(-342, 1105));
    }
}
