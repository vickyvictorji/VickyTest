using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;
    public Text timerTxt;
    int mins;
    int seconds;
    public static float totalTime;//in minutes
    public static float secondsLeft;
    public bool isTimeUp;

	void Start () {
        instance = this;
        totalTime = 1;
        secondsLeft = totalTime * 60;
	}
	
	void Update () {
        if (isTimeUp)
        {
            return;
        }
        secondsLeft = secondsLeft - Time.deltaTime;
        if (secondsLeft > 0)
        {
            mins = (int)(secondsLeft / 60);
            seconds = ((int)secondsLeft % 60);
            timerTxt.text = "" + mins + ":" + seconds;
        }
        else
        {
            isTimeUp = true;
            Player.instance.GameOver();
        }

	}
}
