using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetBlockText : MonoBehaviour {

    public StatsManager stats;
    public int doorsNeeded;
    public string blockName;

	// Use this for initialization
	void Awake ()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(stats.unlockedDoors >= doorsNeeded)
        {
            GetComponent<Text>().text = blockName;
            GetComponent<Text>().color = new Color(1, 1, 0.65f);
        }
        else
        {
            GetComponent<Text>().text = "LOCKED";
            GetComponent<Text>().color = new Color(0.1f, 0.1f, 0.1f);
        }
	}
}
