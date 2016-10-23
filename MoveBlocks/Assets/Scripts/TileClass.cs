using UnityEngine;
using System.Collections;

public class TileClass : MonoBehaviour
{
    public enum TileType
    {
        Wall,
        Floor,
        Target,
        Crate,
        Player,
        Stairs,
        Ice,
    };

    public TileType tileType;
    public Vector2 loc;
    public bool onTarget;
    public bool fake;

    public void UpdatePos(Vector2 position)
    {
        if(GetComponent<PlayerController>() != null)
        {
            GetComponent<PlayerController>().dest = position;
            GetComponent<PlayerController>().canMove = false;
            GetComponent<Animator>().SetTrigger("Push");
            loc = position;
            if (tileType == TileType.Player)
            {
                TileManager tileManager = Camera.main.GetComponent<TileManager>();
                for (int i = 0; i < tileManager.tiles.Count; i++)
                {

                    if (tileManager.tiles[i].loc == loc && tileManager.tiles[i].tileType == TileType.Stairs)
                    {
                        LevelManager levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
                        levelManager.PlayerOnExit(GetComponent<PlayerController>());
                        levelManager._sound.PlayOneShot(levelManager.completeLevel, 0.8f);
                        Debug.Log("ayylmao");
                    }
                }
            }
        }
        else
        {
            transform.position = position;
            loc = new Vector2(Mathf.Round(position.x * 100) / 100, Mathf.Round(position.y * 100) / 100);
            
        }
    }
}
