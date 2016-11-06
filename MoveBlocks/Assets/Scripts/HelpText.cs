using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpText : MonoBehaviour
{

    float timer;
    Text helpText;

    public void Awake()
    {
        helpText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(  timer > 0.2f)
        {
            helpText.text = "H";
        }
        if (timer > 0.4f)
        {
            helpText.text = "He";
        }
        if (timer > 0.6f)
        {
            helpText.text = "Hel";
        }
        if (timer > 0.8f)
        {
            helpText.text = "Help";
        }
        if (timer > 1.0f)
        {
            helpText.text = "Help!";
        }
        if (timer > 1.8)
        {
            helpText.text = "";
        }
        if (timer > 2)
        {
            timer = 0;
        }

    }
}
