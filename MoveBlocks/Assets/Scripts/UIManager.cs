using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject controlPad;
    public GameObject controlPad2;
    public GameObject settingsPanel;
    public LevelManager levelManager;

    public Vector2 padStart1;
    public Vector2 padStart2;

    public GameObject[] keys;

    void Awake()
    {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

    void Start()
    {
        padStart1 = controlPad.GetComponent<RectTransform>().anchoredPosition;
        padStart2 = controlPad2.GetComponent<RectTransform>().anchoredPosition;
    }

	public void OpenPanel()
    {
        levelManager._sound.PlayOneShot(levelManager.openMenu);
        settingsPanel.SetActive(true);
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(false);
        }
    }

    public void ClosePanel()
    {
        levelManager._sound.PlayOneShot(levelManager.closeMenu);
        settingsPanel.SetActive(false);
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(true);
        }
    }

    public void ResetDragPanel()
    {
        levelManager._sound.PlayOneShot(levelManager.button);
        controlPad.GetComponent<RectTransform>().anchoredPosition = padStart1;
        controlPad2.GetComponent<RectTransform>().anchoredPosition = padStart2;
    }

    public void MainMenu()
    {
        levelManager._sound.PlayOneShot(levelManager.button);
        SceneManager.LoadScene(0);
    }

    public void DragPanel(bool secondPlayer)
    {
        Debug.Log("Dragging...");
        if(settingsPanel.activeSelf)
        {
            if(!levelManager.coop)
            {
                controlPad.GetComponent<RectTransform>().anchoredPosition = new Vector2((Input.mousePosition.x - Screen.width / 2) * 1.8f, Input.mousePosition.y - 200);
            }
            else
            {
                if(!secondPlayer)
                {
                    Vector2 pos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<Canvas>().transform as RectTransform, new Vector3(Input.mousePosition.x, Input.mousePosition.y -70, Input.mousePosition.z), GetComponent<Canvas>().worldCamera, out pos);
                    controlPad.transform.position = GetComponent<Canvas>().transform.TransformPoint(pos);


                    //controlPad.GetComponent<RectTransform>().anchoredPosition = new Vector2((Input.mousePosition.x - Screen.width / 2) * 5f, Input.mousePosition.y - (Screen.height / 2));
                }
                else
                {
                    Vector2 pos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<Canvas>().transform as RectTransform, new Vector3(Input.mousePosition.x, Input.mousePosition.y - 70, Input.mousePosition.z), GetComponent<Canvas>().worldCamera, out pos);
                    controlPad2.transform.position = GetComponent<Canvas>().transform.TransformPoint(pos);

                    //controlPad2.GetComponent<RectTransform>().anchoredPosition = new Vector2((Input.mousePosition.x - Screen.width / 2) * 5f, Input.mousePosition.y - (Screen.height / 2));
                }
            }
            
        }
        
    }

}
