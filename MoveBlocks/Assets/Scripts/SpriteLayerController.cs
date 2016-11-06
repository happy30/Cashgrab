using UnityEngine;
using System.Collections;

public class SpriteLayerController : MonoBehaviour
{
    public float player1Y;
    public float player2Y;

    public GameObject player1;
    public GameObject player2;

    public bool coop;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(coop)
        {
            if (player1 != null && player2 != null)
            {
                player1Y = player1.transform.position.y;
                player2Y = player2.transform.position.y;

                if (player1Y > player2Y)
                {
                    player2.GetComponent<SpriteRenderer>().sortingLayerName = "TopPlayer";
                    player1.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                }
                else if (player1Y < player2Y)
                {
                    player2.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                    player1.GetComponent<SpriteRenderer>().sortingLayerName = "TopPlayer";
                }
            }
            else
            {
                AssignPlayers();
                Debug.Log("nulll");
            }
        }
	}

    public void AssignPlayers()
    {
        player1Y = 0;
        player2Y = 0;

        player1 = GameObject.Find("Player(Clone)");
        if(GameObject.Find("Playerf(Clone)"))
        {
            player2 = GameObject.Find("Playerf(Clone)");
        }
    }
}
