using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BigPlayerSpriteController : MonoBehaviour {

    public Sprite bigM;
    public Sprite bigF;
    public enum WhichPlayer
    {
        Player1,
        Player2
    };

    public WhichPlayer whichPlayer;

    public StatsManager stats;

	void Awake ()
    {
        stats = GameObject.Find("Stats").GetComponent<StatsManager>();
	}

    void Start()
    {
        if(stats.male)
        {
            if(whichPlayer == WhichPlayer.Player1)
            {
                GetComponent<Image>().sprite = bigM;
            }
            else if (whichPlayer == WhichPlayer.Player2)
            {
                GetComponent<Image>().sprite = bigF;
            }
        }
        else
        {
            if (whichPlayer == WhichPlayer.Player1)
            {
                GetComponent<Image>().sprite = bigF;
            }
            else if (whichPlayer == WhichPlayer.Player2)
            {
                GetComponent<Image>().sprite = bigM;
            }
        }
    }
}
