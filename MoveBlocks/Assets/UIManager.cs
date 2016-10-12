using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject controlPad;
    public GameObject settingsPanel;

    public GameObject[] keys;

	public void OpenPanel()
    {
        settingsPanel.SetActive(true);
        for(int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(false);
        }
    }

    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(true);
        }
    }

    public void ResetDragPanel()
    {
        controlPad.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DragPanel()
    {
        Debug.Log("Dragging...");
        if(settingsPanel.activeSelf)
        {
            controlPad.GetComponent<RectTransform>().anchoredPosition = new Vector2((Input.mousePosition.x - Screen.width /2) * 1.8f, Input.mousePosition.y);
        }
        
    }
}
