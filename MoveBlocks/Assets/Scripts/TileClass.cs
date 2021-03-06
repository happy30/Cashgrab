﻿using UnityEngine;
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
        Marble
    };

    public TileType tileType;
    public Vector2 loc;
    public bool onTarget;
    public bool fake;
    public bool collected;
    public Vector3 color;

    public LevelManager levelManager;

    public void Start()
    {
        color = new Vector3(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b);
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

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

                    if (tileManager.tiles[i].loc == loc && tileManager.tiles[i].tileType == TileType.Stairs && levelManager.isCompleted)
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

    public void CheckOnTarget()
    {
        TileManager tileManager = Camera.main.GetComponent<TileManager>();
        if (tileType == TileType.Crate && !fake)
        {
            for(int i = 0; i < tileManager.tiles.Count; i++)
            {
                if(tileManager.tiles[i].loc == loc && tileManager.tiles[i].tileType == TileType.Floor || tileManager.tiles[i].loc == loc && tileManager.tiles[i].tileType == TileType.Ice)
                {
                    onTarget = false;
                    GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z);
                }
            }

            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                if (tileManager.tiles[i].loc == loc && tileManager.tiles[i].tileType == TileType.Target)
                {
                    if(tileManager.tiles[i].color == color)
                    {
                        onTarget = true;
                        GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else
                    {
                        onTarget = false;
                        GetComponent<SpriteRenderer>().color = new Color(color.x, color.y, color.z);
                    }
                    
                }
            }
        }
    }

    public void Collect()
    {
        
        GetComponent<SpriteRenderer>().sprite = null;
        collected = true;
    }
}
