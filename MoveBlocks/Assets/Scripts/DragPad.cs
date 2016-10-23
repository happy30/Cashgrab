using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragPad : EventTrigger
{
    public override void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Ondrag called");
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - 64f);
    }
	
}
