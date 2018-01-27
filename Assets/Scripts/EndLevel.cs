using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour {


    //Script ends level when player reaches win collider
    public Text timer;

    //int deathCount = 0;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            PlayerStats.TimerData = timer.text;
            //PlayerStats.Deaths = deathCount;
            //Load Scene
        }
       

    }
}
