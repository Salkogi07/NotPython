using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    public Slider progressbar;
    public Text loadtext;
    public Text tip;
    private float fDestroyTime = 2f;
    private float fTickTime;

    private void Awake()
    {
        StartCoroutine(LoadScene());
        SetTip();
    }
    private void SetTip()
    {
        string[] Word = new string[5];
        Word[0] = "Tip : 정식이는 충주상업고등학교 1학년 5반 입니다.";
        Word[1] = "Tip : 정식이의 생일은 9월 21일 입니다.";
        Word[2] = "Tip : 충주상업고등학교 1학년 5반에는 '가오상현'이 있습니다.";
        Word[3] = "Tip : 병준이는 평상시에 야애니를 봅니다.";
        Word[4] = "Tip : 충상업고등학교 1학년 5반에는 빨갱이가 있습니다.";
        int SetTip = Random.Range(0, 4);
        tip.text = Word[SetTip];
    }
    private void Update()
    {
        fTickTime += Time.deltaTime;

        if (fTickTime >= fDestroyTime)
        {
            SetTip();
            fTickTime = 0f;
        }
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Play");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value,0.9f,Time.deltaTime);
            }else if(operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 3f, Time.deltaTime);
            }

            if(progressbar.value >= 3f)
            {
                loadtext.text = "Press SpaceBar";
            }

            if(Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 3f && operation.progress >= 0.9f) 
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
