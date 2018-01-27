using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    //This script shows a timer.

    float timer = 0.0f;
    Text textBox;

	// Use this for initialization
	void Start () {
        textBox = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - (minutes * 60));
        string displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        textBox.text = displayTime;
	}

    
}
