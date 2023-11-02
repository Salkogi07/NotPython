using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillEnemyCount : MonoBehaviour
{
    public Text TimerText;
    public int killCount = 0;

    void Update()
    {
        TimerText.text = "해치운 회원 수 : " + killCount.ToString();
    }
}
