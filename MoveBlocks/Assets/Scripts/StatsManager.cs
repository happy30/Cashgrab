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

    public static StatsManager Instance;
    public bool male;

    void Awake()
    {
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
        if(PlayerPrefsX.GetBoolArray("UnlockedLevels").Length > 1)
        {
            unlockedLevels = PlayerPrefsX.GetBoolArray("UnlockedLevels");
            clearedLevels = PlayerPrefsX.GetBoolArray("ClearedLevels");
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
            clearedLevels[lv] = true;

            int u = 0;
            for (int i = 0; i < clearedLevels.Length; i++)
            {
                if (clearedLevels[i])
                {
                    u++;
                }
            }
            Debug.Log(u);
            Debug.Log(unlockedLevels.Length);
            if ((u + 4) < 30)
            {
                Debug.Log("cannothapen");
                unlockedLevels[u + 4] = true;
            }

            PlayerPrefsX.SetBoolArray("UnlockedLevels", unlockedLevels);
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
        for(int i = 0; i < unlockedLevels.Length; i ++)
        {
            if (i > 4)
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

        PlayerPrefsX.SetBoolArray("UnlockedLevels", unlockedLevels);
        PlayerPrefsX.SetBoolArray("ClearedLevels", clearedLevels);
        PlayerPrefsX.SetBoolArray("UnlockedLevelsCoop", unlockedLevelsCoop);
        PlayerPrefsX.SetBoolArray("ClearedLevelsCoop", clearedLevelsCoop);
    }
}
