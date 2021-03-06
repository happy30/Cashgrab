﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public TileManager tileManager;
    public LevelManager levelManager;
    public List<Vector2> undoPos = new List<Vector2>();

    public enum Gender
    {
        Male,
        Female
    };

    public Gender gender;

    public Vector2 dest;
    Vector2 lastPos;
    Vector2 deltaPos;

    public bool canMove;
    public bool isSliding;
    public bool completedCoop;
    public OptionsSettings options;

    void Awake()
    {
        
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        levelManager.undo.SetActive(false);
        tileManager = Camera.main.GetComponent<TileManager>();
        dest = transform.position;
        options = GameObject.Find("Stats").GetComponent<OptionsSettings>();
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
        if(Vector2.Distance(transform.position, dest) < 0.1f && !completedCoop)
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
        else if (completedCoop)
        {
            transform.position = new Vector2(-1500, 1500);
            GetComponent<TileClass>().loc = new Vector2(-1500, 1500);
        }
    }

    public void SavePoses()
    {
        levelManager.undo.SetActive(true);
        undoPos.Clear();
            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                undoPos.Add(tileManager.tiles[i].loc);
            }

    }

    public void Undo()
    {
        levelManager._sound.PlayOneShot(levelManager.move, options.SFXFactor);
        levelManager.undo.SetActive(false);
        if(!levelManager.addExit)
        {
            for (int i = 0; i < tileManager.tiles.Count; i++)
            {
                tileManager.tiles[i].loc = undoPos[i];
                tileManager.tiles[i].UpdatePos(tileManager.tiles[i].loc);
                tileManager.tiles[i].CheckOnTarget();
            }
        }
        isSliding = false;
    }

    public void MoveRight()
    {
        if(!isSliding)
        {
            SavePoses();
        }
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
                GetComponent<TileClass>().loc = new Vector2(Mathf.Round(dest.x * 100) / 100, Mathf.Round(dest.y * 100) / 100);

            }
            else
            if (!levelManager._sound.isPlaying)
            {
                levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
            }
        }
        
    }

    public void MoveLeft()
    {
        if (!isSliding)
        {
            SavePoses();
        }
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x - 1 * 0.64f, transform.position.y), new Vector2(transform.position.x - 2 * 0.64f, transform.position.y)))
            {
                //transform.position = new Vector2(transform.position.x - 1 * 0.64f, transform.position.y);
                lastPos = transform.position;
                dest = new Vector2(transform.position.x - 1 * 0.64f, transform.position.y);
                canMove = false;
                GetComponent<TileClass>().loc = new Vector2(Mathf.Round(dest.x * 100) / 100, Mathf.Round(dest.y * 100) / 100);

            }
            else
            {
                if(!levelManager._sound.isPlaying)
                {
                    levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                }
            }
        }
        
    }

    public void MoveUp()
    {
        if (!isSliding)
        {
            SavePoses();
        }
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x, transform.position.y + 1 * 0.64f), new Vector2(transform.position.x, transform.position.y + 2 * 0.64f)))
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y + 1 * 0.64f);
                lastPos = transform.position;
                dest = new Vector2(transform.position.x, transform.position.y + 1 * 0.64f);
                canMove = false;
                GetComponent<TileClass>().loc = new Vector2(Mathf.Round(dest.x * 100) / 100, Mathf.Round(dest.y * 100) / 100);

            }
            else
            if (!levelManager._sound.isPlaying)
            {
                levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
            }
        }
    }

    public void MoveDown()
    {
        if (!isSliding)
        {
            SavePoses();
        }
        CheckProgress();
        if (!levelManager.isCompleted && canMove)
        {
            if (CheckForCollision(new Vector2(transform.position.x, transform.position.y - 1 * 0.64f), new Vector2(transform.position.x, transform.position.y - 2 * 0.64f)))
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y - 1 * 0.64f);
                lastPos = transform.position;
                dest = new Vector2(transform.position.x, transform.position.y - 1 * 0.64f);
                canMove = false;
                GetComponent<TileClass>().loc = new Vector2(Mathf.Round(dest.x * 100) / 100, Mathf.Round(dest.y * 100) / 100);

            }
            else
            {
                if (!levelManager._sound.isPlaying)
                {
                    levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                }
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
        for(int i = 0; i < tileManager.tiles2.Count; i ++)
        {
            if(tileManager.tiles2[i].loc == pos && tileManager.tiles2[i].tileType == TileClass.TileType.Marble)
            {
                for(int x = 0; x < tileManager.tiles.Count; x++)
                {
                    if(tileManager.tiles[x].loc == pos && tileManager.tiles[x].tileType == TileClass.TileType.Crate)
                    {
                        for (int n = 0; n < tileManager.tiles.Count; n++)
                        {
                            if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Crate || 
                                tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Wall)
                            {
                                return false;
                            }
                        }
                    }
                }


                
                if(!tileManager.tiles2[i].collected)
                {
                    levelManager.UIMarbles[levelManager.marbleProgress].GetComponent<Image>().color = Color.white;
                    levelManager.marbleProgress++;
                    levelManager._sound.PlayOneShot(levelManager.onTarget, options.SFXFactor);
                    tileManager.tiles2[i].Collect();
                }
            }
        }

        for(int i = 0; i < tileManager.tiles.Count; i++)
        {
            if(tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Wall)
            {
                Debug.Log("We hit a wall");
                isSliding = false;
                return false;
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Stairs && 
                tileManager.tiles[i].gameObject.activeSelf)
            {
                Debug.Log("completelevel");
                levelManager.PlayerOnExit(this);
                levelManager._sound.PlayOneShot(levelManager.completeLevel, 0.8f * options.SFXFactor);
                return true;
            }
            if(tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Crate || 
                tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Player)
            {
                for(int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Crate || 
                        tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Wall || 
                        tileManager.tiles[n].loc == pos && tileManager.tiles[n].tileType == TileClass.TileType.Target 
                        
                        && 

                        levelManager.addExit || 
                        tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Player)
                    {
                        Debug.Log("We cant move the crate123");
                        isSliding = false;
                        return false;
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Floor || 
                        tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Ice)
                    {
                        if(!levelManager.addExit && !tileManager.tiles[i].fake)
                        {
                            levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                            Debug.Log("We moved a crate");
                            
                            tileManager.tiles[i].UpdatePos(pos2);
                            tileManager.tiles[i].onTarget = false;
                            tileManager.tiles[i].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[i].GetComponent<TileClass>().color.x, tileManager.tiles[i].GetComponent<TileClass>().color.y, tileManager.tiles[i].GetComponent<TileClass>().color.z) ;
                            isSliding = false;
                            return true;
                        }
                        if(tileManager.tiles[i].fake)
                        {
                            levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                            Debug.Log("We moved a crate");
                            
                            tileManager.tiles[i].UpdatePos(pos2);
                            tileManager.tiles[i].onTarget = false;
                            tileManager.tiles[i].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[i].GetComponent<TileClass>().color.x, tileManager.tiles[i].GetComponent<TileClass>().color.y, tileManager.tiles[i].GetComponent<TileClass>().color.z);
                            isSliding = false;
                            return true;
                        }
                        
                    }
                }
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Target)
                    {
                        Debug.Log("We moved a crate ON A TARGET1");
                        
                        if (!levelManager.addExit && !tileManager.tiles[i].fake)
                        {
                            if(tileManager.tiles[i].GetComponent<TileClass>().color == tileManager.tiles[n].GetComponent<TileClass>().color)
                            {
                                
                                levelManager._sound.PlayOneShot(levelManager.onTarget, options.SFXFactor);
                                tileManager.tiles[i].onTarget = true;
                                tileManager.tiles[i].GetComponent<SpriteRenderer>().color = Color.blue;
                                tileManager.tiles[i].UpdatePos(pos2);
                                CheckProgress();
                                return true;
                            }
                            else
                            {
                                
                                tileManager.tiles[i].UpdatePos(pos2);
                                tileManager.tiles[i].onTarget = false;
                                tileManager.tiles[i].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[i].GetComponent<TileClass>().color.x, tileManager.tiles[i].GetComponent<TileClass>().color.y, tileManager.tiles[i].GetComponent<TileClass>().color.z);
                                levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                return true;
                            }
                            
                        }
                        else if(tileManager.tiles[i].fake)
                        {
                            
                            tileManager.tiles[i].UpdatePos(pos2);
                            levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                            return true;
                        }
                    }
                }
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Ice)
            {
                for (int g = 0; g < tileManager.tiles.Count; g++)
                {
                    if (tileManager.tiles[g].loc == pos && tileManager.tiles[g].tileType == TileClass.TileType.Crate || tileManager.tiles[g].loc == pos && tileManager.tiles[g].tileType == TileClass.TileType.Player)
                    {
                        for(int h = 0; h < tileManager.tiles.Count; h++)
                        {
                            if(tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Wall || tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Crate || tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Player)
                            {
                                Debug.Log("cant move");
                                levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                isSliding = false;
                                return false;
                            }
                        }
                        for (int h = 0; h < tileManager.tiles.Count; h++)
                        {
                            if (tileManager.tiles[h].loc == pos2 && tileManager.tiles[h].tileType == TileClass.TileType.Target)
                            {
                                if (!levelManager.addExit && !tileManager.tiles[g].fake)
                                {
                                    if(tileManager.tiles[g].GetComponent<TileClass>().color == tileManager.tiles[h].GetComponent<TileClass>().color)
                                    {
                                        tileManager.tiles[g].onTarget = true;
                                        tileManager.tiles[g].GetComponent<SpriteRenderer>().color = Color.blue;
                                        tileManager.tiles[g].UpdatePos(pos2);
                                        levelManager._sound.PlayOneShot(levelManager.onTarget, options.SFXFactor);
                                    }
                                    else
                                    {
                                        tileManager.tiles[g].onTarget = false;
                                        tileManager.tiles[g].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[g].GetComponent<TileClass>().color.x, tileManager.tiles[g].GetComponent<TileClass>().color.y, tileManager.tiles[g].GetComponent<TileClass>().color.z);
                                        tileManager.tiles[g].UpdatePos(pos2);
                                        levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                    }
                                }
                                Debug.Log("We moved a crate ON A TARGET2");
                                
                                if (tileManager.tiles[g].fake)
                                {
                                    
                                    tileManager.tiles[g].UpdatePos(pos2);
                                    levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                }
                                CheckProgress();
                                return true;
                            }
                        }
                        levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                        Debug.Log("move crate on ice");
                        
                        tileManager.tiles[g].UpdatePos(pos2);
                        tileManager.tiles[g].onTarget = false;
                        tileManager.tiles[g].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[g].GetComponent<TileClass>().color.x, tileManager.tiles[g].GetComponent<TileClass>().color.y, tileManager.tiles[g].GetComponent<TileClass>().color.z);
                        return true;
                    }
                }
                Debug.Log("Sliding!");
                isSliding = true;
                levelManager._sound.PlayOneShot(levelManager.move, 0.5f * options.SFXFactor);
                return true;
            }
            if (tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Floor && isSliding || tileManager.tiles[i].loc == pos && tileManager.tiles[i].tileType == TileClass.TileType.Target && isSliding)
            {
                for (int n = 0; n < tileManager.tiles.Count; n++)
                {
                    if (tileManager.tiles[n].loc == pos2 && tileManager.tiles[n].tileType == TileClass.TileType.Floor)
                    {
                        for (int k = 0; k < tileManager.tiles.Count; k++)
                        {
                            if (tileManager.tiles[k].loc == pos && tileManager.tiles[k].tileType == TileClass.TileType.Crate || tileManager.tiles[k].loc == pos && tileManager.tiles[k].tileType == TileClass.TileType.Player)
                            {
                                if(!levelManager.addExit && !tileManager.tiles[k].fake)
                                {
                                    for(int p = 0; p < tileManager.tiles.Count; p++)
                                    {
                                        if(tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Crate || tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Player)
                                        {
                                            Debug.Log("cant move");
                                            levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                            isSliding = false;
                                            return false;
                                        }
                                    }
                                    levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                    Debug.Log("We moved a crate ON A TARGET ripkip2");
                                    tileManager.tiles[k].UpdatePos(pos2);

                                    tileManager.tiles[k].onTarget = false;
                                    tileManager.tiles[k].GetComponent<SpriteRenderer>().color = new Color(tileManager.tiles[k].GetComponent<TileClass>().color.x, tileManager.tiles[k].GetComponent<TileClass>().color.y, tileManager.tiles[k].GetComponent<TileClass>().color.z);

                                    for (int d = 0; d < tileManager.tiles.Count; d++)
                                    {
                                        if (tileManager.tiles[d].loc == pos2 && tileManager.tiles[d].tileType == TileClass.TileType.Target)
                                        {
                                            if(tileManager.tiles[k].GetComponent<TileClass>().color == tileManager.tiles[d].GetComponent<TileClass>().color)
                                            {
                                                levelManager._sound.PlayOneShot(levelManager.onTarget, options.SFXFactor);
                                                Debug.Log("blue ice");
                                                tileManager.tiles[k].onTarget = true;
                                                tileManager.tiles[k].GetComponent<SpriteRenderer>().color = Color.blue;
                                                //isSliding = false;
                                                CheckProgress();
                                                return true;
                                            }
                                            else
                                            {
                                                for (int p = 0; p < tileManager.tiles.Count; p++)
                                                {
                                                    if (tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Crate || tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Player)
                                                    {
                                                        Debug.Log("cant move");
                                                        levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                                        isSliding = false;
                                                        return false;
                                                    }
                                                }
                                                levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                                Debug.Log("We moved a crate ON A TARGET ripkip");
                                                tileManager.tiles[k].UpdatePos(pos2);
                                                for (int q = 0; q < tileManager.tiles.Count; q++)
                                                {
                                                    if (tileManager.tiles[q].loc == pos2 && tileManager.tiles[q].tileType == TileClass.TileType.Target)
                                                    {
                                                        
                                                        Debug.Log("blue ice");
                                                        //tileManager.tiles[k].onTarget = true;
                                                        //tileManager.tiles[k].GetComponent<SpriteRenderer>().color = Color.blue;
                                                        return true;
                                                    }
                                                }
                                                CheckProgress();
                                                return false;
                                            }

                                            
                                        }
                                    }
                                    CheckProgress();
                                    return false;
                                }
                                else if(tileManager.tiles[k].fake)
                                {
                                    for (int p = 0; p < tileManager.tiles.Count; p++)
                                    {
                                        if (tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Crate || tileManager.tiles[p].loc == pos2 && tileManager.tiles[p].tileType == TileClass.TileType.Player)
                                        {
                                            Debug.Log("cant move");
                                            levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                            isSliding = false;
                                            return false;
                                        }
                                    }
                                    levelManager._sound.PlayOneShot(levelManager.push, options.SFXFactor);
                                    Debug.Log("We moved a crate ON A TARGET ripkip");
                                    tileManager.tiles[k].UpdatePos(pos2);
                                    for (int d = 0; d < tileManager.tiles.Count; d++)
                                    {
                                        if (tileManager.tiles[d].loc == pos2 && tileManager.tiles[d].tileType == TileClass.TileType.Target)
                                        {
                                            
                                            Debug.Log("blue ice");
                                            //tileManager.tiles[k].onTarget = true;
                                            //tileManager.tiles[k].GetComponent<SpriteRenderer>().color = Color.blue;
                                            return true;
                                        }
                                    }
                                    CheckProgress();
                                    return false;
                                }
                            }
                        }
                        Debug.Log("ufucked");
                        isSliding = false;

                        for(int t = 0; t < tileManager.tiles.Count; t++)
                        {
                            if(tileManager.tiles[t].loc == pos && tileManager.tiles[t].tileType == TileClass.TileType.Crate)
                            {
                                if(!tileManager.tiles[t].fake)
                                {
                                    for (int q = 0; q < tileManager.tiles.Count; q++)
                                    {
                                        if (tileManager.tiles[q].loc == pos && tileManager.tiles[q].tileType == TileClass.TileType.Target)
                                        {
                                            if (levelManager.addExit)
                                            {
                                                Debug.Log("Cant move after level finished");
                                                levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                                return false;
                                            }
                                        }
                                    }
                                }
                                else if (tileManager.tiles[t].fake)
                                {
                                    for (int q = 0; q < tileManager.tiles.Count; q++)
                                    {
                                        if (tileManager.tiles[q].loc == pos && tileManager.tiles[q].tileType == TileClass.TileType.Target)
                                        {
                                            if (levelManager.addExit)
                                            {
                                                Debug.Log("Cant move after level finished");
                                                levelManager._sound.PlayOneShot(levelManager.blocked, options.SFXFactor);
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                        levelManager._sound.PlayOneShot(levelManager.move, options.SFXFactor);
                        return true;
                    }
                }
                isSliding = false;
            }
        }
        
        levelManager._sound.PlayOneShot(levelManager.move, options.SFXFactor);
        return true;
    }
}
