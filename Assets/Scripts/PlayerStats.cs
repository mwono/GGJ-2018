using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {

    //This script saves player data.

    private static int deaths;
    private static string timerData;


    public static int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    public static string TimerData
    {
        get
        {
            return timerData;
        }
        set
        {
            timerData = value;
        }
    }
}
