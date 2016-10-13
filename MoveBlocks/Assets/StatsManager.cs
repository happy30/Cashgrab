using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {

    public int totalLevels;
    public bool[] unlockedLevels;
    public bool[] clearedLevels;

    public int levelToLoad;

    public static StatsManager Instance;

    void Awake()
    {
        if(Instance)
        {
            DestroyImmediate(gameObject);
        }    
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

	
    public void ClearLevel(int lv)
    {
        clearedLevels[lv] = true;

        int u = 0;
        for (int i = 0; i < clearedLevels.Length; i++)
        {
            if(clearedLevels[i])
            {
                u++;
            }
        }
        unlockedLevels[u + 4] = true;
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
