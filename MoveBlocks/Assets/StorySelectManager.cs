using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StorySelectManager : MonoBehaviour
{
    public Text totalKeys;
    public StatsManager stats;

    void Awake()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
    }


    void Update()
    {
        totalKeys.text = ": " + stats.keys.ToString();
    }

}
