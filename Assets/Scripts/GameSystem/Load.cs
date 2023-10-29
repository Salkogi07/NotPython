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
        Word[0] = "Tip : �����̴� ���ֻ������б� 1�г� 5�� �Դϴ�.";
        Word[1] = "Tip : �������� ������ 9�� 21�� �Դϴ�.";
        Word[2] = "Tip : ���ֻ������б� 1�г� 5�ݿ��� '��������'�� �ֽ��ϴ�.";
        Word[3] = "Tip : �����̴� ���ÿ� �߾ִϸ� ���ϴ�.";
        Word[4] = "Tip : ��������б� 1�г� 5�ݿ��� �����̰� �ֽ��ϴ�.";
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
