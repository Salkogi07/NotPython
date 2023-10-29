using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    bool TimerOn;
    public Text TimerText;
    float time;

    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            time += Time.deltaTime;
            TimerText.text = "플레이 타임 : "+
                ((int)time / 3600).ToString("D2") + ":"+
                ((int)time / 60 % 60).ToString("D2") +":"+
                ((int)time % 60).ToString("D2");
        }
    }

    public void SetTimerOn()
    {
        TimerOn = true;
    }

    public void SetTimerStop()
    {
        TimerOn = false;
    }
}
