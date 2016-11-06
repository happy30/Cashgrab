using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour
{

    public float moveSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(transform.GetComponent<RectTransform>().anchoredPosition.x - moveSpeed * Time.deltaTime, transform.GetComponent<RectTransform>().anchoredPosition.y);

        if(transform.GetComponent<RectTransform>().anchoredPosition.x < -800)
        {
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, transform.GetComponent<RectTransform>().anchoredPosition.y);
        }
	}
}
