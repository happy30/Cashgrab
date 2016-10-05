using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Vector2 startPos;
    public int tilePos;

    public RectTransform pos;
    public float offset;

    public TileManager tileManager;

    void Awake()
    {
        pos = GetComponent<RectTransform>();
        tileManager = Camera.main.GetComponent<TileManager>();
    }

	// Use this for initialization
	void Start ()
    {
       offset = 32.5f;
       pos.anchoredPosition = new Vector2((startPos.x * 65) + offset, (startPos.y * 65) + offset);
       for(int x = 0; x < 16; x++)
       {
           for(int y = 0; y < 16; y++)
           {
               if(startPos == new Vector2(x, y))
               {
                   tilePos = (x * 16) + y;
                   break;
               }
           }
       }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        for (int i = 0; i < tileManager.tiles.Count; i++)
        {
            if (tileManager.tiles[i].GetComponent<TileClass>().pos == tilePos + 1)
            {
                if (tileManager.tiles[i].GetComponent<TileClass>().tileType == TileClass.TileType.Crate)
                {
                    for (int n = 0; n < tileManager.tiles.Count; n++)
                    {
                        if (tileManager.tiles[n].GetComponent<TileClass>().pos == tilePos + 2)
                        {
                            if (tileManager.tiles[n].GetComponent<TileClass>().tileType == TileClass.TileType.Floor)
                            {
                                tileManager.tiles[i].GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y + 65), 2);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (tileManager.tiles[i].GetComponent<TileClass>().tileType == TileClass.TileType.Floor)
                {
                    pos.anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y + 65), 2);
                    tilePos++;
                    break;
                }
            }
            

            /*if(tileManager.tiles[tilePos + 1].GetComponent<TileClass>().tileType != TileClass.TileType.Wall)
            {
                if (tileManager.tiles[tilePos + 1].GetComponent<TileClass>().tileType == TileClass.TileType.Crate)
                {
                    tileManager.tiles[tilePos + 1].GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y + 65), 2);
                }

                pos.anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y + 65), 2);
                tilePos++;
            }*/
        }
    }

    void MoveLeft()
    {
        if (tileManager.tiles[tilePos - 16].GetComponent<TileClass>().tileType != TileClass.TileType.Wall)
        {
            pos.anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x - 65, pos.anchoredPosition.y), 200f);
            tilePos -= 16;
        }
        
    }

    void MoveRight()
    {
        if (tileManager.tiles[tilePos + 16].GetComponent<TileClass>().tileType != TileClass.TileType.Wall)
        {
            pos.anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x + 65, pos.anchoredPosition.y), 2);
            tilePos += 16;
        }
         
    }

    void MoveDown()
    {
        if (tileManager.tiles[tilePos - 1].GetComponent<TileClass>().tileType != TileClass.TileType.Wall)
        {
            pos.anchoredPosition = Vector2.Lerp(pos.anchoredPosition, new Vector2(pos.anchoredPosition.x, pos.anchoredPosition.y - 65), 2);
            tilePos--;
        }
            
    }
}
