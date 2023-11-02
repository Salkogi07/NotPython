using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameObject targetObj;

    public GameObject[] toObj;
    public GameObject[] spawnManger;
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public int mapNum;

    private void Start()
    {
        spawnEnemy();
    }
    private void spawnEnemy()
    {
        spawnManger[mapNum].SetActive(true);
    }

    IEnumerator TeleportRoutine()
    {
        yield return null;
        targetObj.GetComponent<Player>().isControl = false;
        targetObj.GetComponent<PlayerMove>().isControl = false;
        targetObj.GetComponent<PlayerAttack>().isControl = false;

        yield return StartCoroutine(FadeFlowIn());

        targetObj.transform.position = toObj[mapNum].transform.position;
        mapNum++;
        spawnEnemy();
        Camera.main.GetComponent<CameraFollow>().ChangeLimit(mapNum);
        yield return StartCoroutine(FadeFlowOut());

        targetObj.GetComponent<Player>().isControl = true;
        targetObj.GetComponent<PlayerMove>().isControl = true;
        targetObj.GetComponent<PlayerAttack>().isControl = true;
    }

    IEnumerator FadeFlowIn()
    {
        Color alpha = Panel.color;

        //Fade IN
        time = 0f;
        Panel.gameObject.SetActive(true);
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator FadeFlowOut()
    {
        Color alpha = Panel.color;

        //Fade IN
        time = 0f;
        Panel.gameObject.SetActive(true);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
    }
}
