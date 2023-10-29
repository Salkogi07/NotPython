using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossJungsikUI : MonoBehaviour
{
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private Text text;

    public BossJungsik boss;

    float imsiSlider;
    string imsiText;
    void Start()
    {
        imsiText = boss.currentHp + " / " + boss.bossHp;
        imsiSlider = (float)boss.currentHp / (float)boss.bossHp;
    }

    void Update()
    {
        imsiSlider = (float)boss.currentHp / (float)boss.bossHp;
        imsiText = boss.currentHp + " / " + boss.bossHp;
        HandleHp();
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, imsiSlider, Time.deltaTime * 10);
        text.text = imsiText;
    }
}
