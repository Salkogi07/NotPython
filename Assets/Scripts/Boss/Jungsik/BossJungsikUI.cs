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
    public BossJungsik[] boss;

    float imsiSlider;
    string imsiText;
    public int page = 0;

    void Start()
    {
        stageManager = GetComponent<StageManager>();
        if(stageManager.mapNum == 2)
        {
            bossUI.gameObject.SetActive(true);
            imsiText = boss[page].currentHp + " / " + boss[page].bossHp;
            imsiSlider = (float)boss[page].currentHp / (float)boss[page].bossHp;
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
            imsiSlider = (float)boss[page].currentHp / (float)boss[page].bossHp;
            imsiText = boss[page].currentHp + " / " + boss[page].bossHp;
            HandleHp();
            if (boss[page].currentHp == 0)
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
