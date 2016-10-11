using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public int dir;
    public PlayerController player;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
	}
	
	void OnTouchDown()
    {
        player.MoveLeft();
    }
}
