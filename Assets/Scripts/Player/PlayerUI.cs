using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private Text text;

    public Player player;

    float imsiSlider;
    string imsiText;
    void Start()
    {
        imsiText = player.currentHp + " / " + player.playerHp;
        imsiSlider = (float)player.currentHp / (float)player.playerHp;
    }

    void Update()
    {
        imsiSlider = (float)player.currentHp / (float)player.playerHp;
        imsiText = player.currentHp + " / " + player.playerHp;
        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, imsiSlider, Time.deltaTime * 10);
        text.text = imsiText;
    }
}
