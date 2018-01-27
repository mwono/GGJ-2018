using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Events : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
