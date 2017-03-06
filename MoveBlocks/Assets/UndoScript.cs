using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoScript : MonoBehaviour {

	public void Undo()
    {
        GameObject.Find("Player(Clone)").GetComponent<PlayerController>().Undo();
    }
}
