using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public TileManager tileManager;
    public LevelManager levelManager;

    Vector2 dest;
    Vector2 lastPos;
    Vector2 deltaPos;

    public bool canMove;
    public bool isSliding;

    void Awake()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        tileManager = Camera.main.GetComponent<TileManager>();
        dest = transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && !isSliding)
        {
            MoveRight();
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isSliding)
        {
            MoveLeft();
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isSliding)
        {
            MoveUp();
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            MoveDown();
            
        }

        transform.position = Vector2.Lerp(transform.position, dest, 20f * Time.deltaTime);
        if(Vector2.Distance(transform.position, dest) < 0.1f)
        {
            transform.position = dest;
            canMove = true;
            if (isSliding)
            {
                deltaPos = dest - lastPos;
                deltaPos = new Vector2(Mathf.Round(deltaPos.x * 10) / 10, Mathf.Round(deltaPos.y * 10) / 10);
                Debug.Log(deltaPos.y);
                if (deltaPos.y == 0.6f)
                {
                    MoveUp();
                }
                if (deltaPos.y == -0.6f)
                {
                    MoveDown();
                }
                if (deltaPos.x == 0.6f)
                {
                    MoveRight();
                }
                if (deltaPos.x == -0.6f)
                {
                    MoveLeft();
                }

            } 
            
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
                lastPos = transform.position;
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
                lastPos = transform.position;
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
                lastPos = transform.position;
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
                lastPos = transform.position;
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
                isSliding = false;
                return false;
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Stairs && tileManager.tiles[i].gameObject.activeSelf)
            {
                Debug.Log("completelevel");
                levelManager.isCompleted = true;
                return true;
            }
            if(tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Crate)
            {
                for(int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Crate || tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Wall)
                    {
                        Debug.Log("We cant move the crate");
                        isSliding = false;
                        return false;
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Floor && !levelManager.addExit || tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Ice && !levelManager.addExit)
                    {
                        Debug.Log("We moved a crate");
                        tileManager.tiles[i].UpdatePos(pos2);
                        tileManager.tiles[i].onTarget = false;
                        tileManager.tiles[i].GetComponent<SpriteRenderer>().color = Color.white;
                        isSliding = false;
                        return true;
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Target && !levelManager.addExit)
                    {
                        Debug.Log("We moved a crate ON A TARGET");
                        tileManager.tiles[i].UpdatePos(pos2);
                        tileManager.tiles[i].onTarget = true;
                        tileManager.tiles[i].GetComponent<SpriteRenderer>().color = Color.blue;
                        CheckProgress();
                        return true;
                    }
                }
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Ice)
            {
                for (int g = 0; g < tileManager.tiles.Count; g++)
                {
                    if (tileManager.tiles[g].loc == pos && tileManager.tiles[g].tileType == TileClass.TileType.Crate)
                    {
                        for(int h = 0; h < tileManager.tiles.Count; h++)
                        {
                            if(tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Wall || tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Crate)
                            {
                                Debug.Log("cant move");
                                isSliding = false;
                                return false;
                            }
                        }
                        Debug.Log("move crate on ice");
                        tileManager.tiles[g].UpdatePos(pos2);
                        tileManager.tiles[g].onTarget = false;
                        tileManager.tiles[g].GetComponent<SpriteRenderer>().color = Color.white;
                        return true;
                    }
                    else
                    {

                    }
                }
                isSliding = true;
                return true;
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Floor && isSliding)
            {
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Floor)
                    {
                        Debug.Log("ufucked");
                        isSliding = false;
                        return false;
                    }
                }
            }
        }
        return true;
    }
	
}
