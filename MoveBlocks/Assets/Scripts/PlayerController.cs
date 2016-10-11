using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public TileManager tileManager;
    public LevelManager levelManager;

    Vector2 dest;
    public bool canMove;

    void Awake()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        tileManager = Camera.main.GetComponent<TileManager>();
        dest = transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
            
        }

        transform.position = Vector2.Lerp(transform.position, dest, 20f * Time.deltaTime);
        if(Vector2.Distance(transform.position, dest) < 0.1f)
        {
            transform.position = dest;
            canMove = true;
        }
    }

    public void MoveRight()
    {
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x + 1 * 0.64f, transform.position.y), new Vector2(transform.position.x + 2 * 0.64f, transform.position.y)))
            {
                //transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x + 1 * 0.64f, transform.position.y), 3f * Time.deltaTime);
                //transform.position = new Vector2(transform.position.x + 1 * 0.64f, transform.position.y);
                dest = new Vector2(transform.position.x + 1 * 0.64f, transform.position.y);
                canMove = false;
            }
        }
        
    }

    public void MoveLeft()
    {
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x - 1 * 0.64f, transform.position.y), new Vector2(transform.position.x - 2 * 0.64f, transform.position.y)))
            {
                //transform.position = new Vector2(transform.position.x - 1 * 0.64f, transform.position.y);
                dest = new Vector2(transform.position.x - 1 * 0.64f, transform.position.y);
                canMove = false;
            }
        }
        
    }

    public void MoveUp()
    {
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x, transform.position.y + 1 * 0.64f), new Vector2(transform.position.x, transform.position.y + 2 * 0.64f)))
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y + 1 * 0.64f);
                dest = new Vector2(transform.position.x, transform.position.y + 1 * 0.64f);
                canMove = false;
            }
        }
    }

    public void MoveDown()
    {
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x, transform.position.y - 1 * 0.64f), new Vector2(transform.position.x, transform.position.y - 2 * 0.64f)))
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y - 1 * 0.64f);
                dest = new Vector2(transform.position.x, transform.position.y - 1 * 0.64f);
                canMove = false;
            }
        }
    }

    void CheckProgress()
    {
        levelManager.progress = 0;
        for(int i = 0; i < tileManager.tiles.Count; i++)
        {
            if (tileManager.tiles[i].GetComponent<SpriteRenderer>().color == Color.blue)
            {
                levelManager.progress++;
            }
        }
    }

    bool CheckForCollision(Vector2 pos, Vector2 pos2)
    {
        for(int i = 0; i < tileManager.tiles.Count; i++)
        {
            if(tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Wall)
            {
                Debug.Log("We hit a wall");
                return false;
            }
            else if(tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Crate)
            {
                for(int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Crate)
                    {
                        Debug.Log("We cant move the crate");
                        return false;
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Floor)
                    {
                        Debug.Log("We moved a crate");
                        tileManager.tiles[i].UpdatePos(pos2);
                        tileManager.tiles[i].onTarget = false;
                        tileManager.tiles[i].GetComponent<SpriteRenderer>().color = Color.white;
                        return true;
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Target)
                    {
                        Debug.Log("We moved a crate ON A TARGET");
                        tileManager.tiles[i].UpdatePos(pos2);
                        tileManager.tiles[i].onTarget = true;
                        tileManager.tiles[i].GetComponent<SpriteRenderer>().color = Color.blue;
                        CheckProgress();
                        return true;
                    }
                }
                return false;
            }
        }
        return true;
    }
	
}
