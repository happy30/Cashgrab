using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenerateLevel : MonoBehaviour {

    public Texture2D[] levels;
    public float offset;

    GameObject generatedTile;
    public GameObject Empty;
    public GameObject floor;
    public GameObject wall;
    public GameObject crate;
    public GameObject target;

    public Transform game;
    public TileManager tileManager;
    public GameObject player;


    void Awake()
    {
        player = GameObject.Find("Player");
        tileManager = Camera.main.GetComponent<TileManager>();
    }
	// Use this for initialization
	void Start ()
    {
        offset = 32.5f;
        Generate(levels[0]);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Generate(Texture2D tex)
    {
        for(int x = 0; x < 16; x++)
        {
            for(int y = 0; y< 16; y++)
            {
                Debug.Log(tex.GetPixel(x, y).r);
                if (tex.GetPixel(x, y).r == 0)
                {
                    generatedTile = (GameObject)Instantiate(Empty);
                    generatedTile.transform.SetParent(game);
                    generatedTile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    generatedTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x * 65) + offset, (y * 65) + offset);
                    generatedTile.GetComponent<TileClass>().pos = (x * 16) + y;
                    tileManager.tiles.Add(generatedTile);
                }
                if (tex.GetPixel(x, y).r > 0.5f && tex.GetPixel(x, y).r < 0.8f) 
                {
                    Debug.Log(tex.GetPixel(x, y).r);
                    generatedTile = (GameObject)Instantiate(wall);
                    generatedTile.transform.SetParent(game);
                    generatedTile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    generatedTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x * 65) + offset, (y * 65) + offset);
                    generatedTile.GetComponent<TileClass>().pos = (x * 16) + y;
                    tileManager.tiles.Add(generatedTile);
                }
                if (tex.GetPixel(x, y).r > 0.2f && tex.GetPixel(x, y).r < 0.4f)
                {
                    generatedTile = (GameObject)Instantiate(floor);
                    generatedTile.transform.SetParent(game);
                    generatedTile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    generatedTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x * 65) + offset, (y * 65) + offset);
                    generatedTile.GetComponent<TileClass>().pos = (x * 16) + y;
                    tileManager.tiles.Add(generatedTile);
                }
                if (tex.GetPixel(x, y).r == 1)
                {
                    generatedTile = (GameObject)Instantiate(target);
                    generatedTile.transform.SetParent(game);
                    generatedTile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    generatedTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x * 65) + offset, (y * 65) + offset);
                    generatedTile.GetComponent<TileClass>().pos = (x * 16) + y;
                    tileManager.tiles.Add(generatedTile);
                }
                if (tex.GetPixel(x, y).b == 1)
                {
                    generatedTile = (GameObject)Instantiate(crate);
                    generatedTile.transform.SetParent(game);
                    generatedTile.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    generatedTile.GetComponent<RectTransform>().anchoredPosition = new Vector2((x * 65) + offset, (y * 65) + offset);
                    generatedTile.GetComponent<TileClass>().pos = (x * 16) + y;
                    tileManager.tiles.Add(generatedTile);
                }
            }
        }
        player.transform.SetAsLastSibling();
        
    }
}
