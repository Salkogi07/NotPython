using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossJungsikUI : MonoBehaviour
{
    [SerializeField]
    private GameObject bossUI;
    [SerializeField]
    private Slider hpbar;
    [SerializeField]
    private Text text;
    

    StageManager stageManager;
    public BossJungsik boss;

    float imsiSlider;
    string imsiText;

    void Start()
    {
        stageManager = GetComponent<StageManager>();
        if(stageManager.mapNum == 2)
        {
            bossUI.gameObject.SetActive(true);
            imsiText = boss.currentHp + " / " + boss.bossHp;
            imsiSlider = (float)boss.currentHp / (float)boss.bossHp;
        }
        else
        {
            bossUI.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (stageManager.mapNum == 2)
        {
            bossUI.gameObject.SetActive(true);
            imsiSlider = (float)boss.currentHp / (float)boss.bossHp;
            imsiText = boss.currentHp + " / " + boss.bossHp;
            HandleHp();
            if (boss.currentHp == 0)
            {
                hpbar.enabled = false;
            }
        }
    }

    private void HandleHp()
    {
        hpbar.value = Mathf.Lerp(hpbar.value, imsiSlider, Time.deltaTime * 10);
        text.text = imsiText;
    }
}
