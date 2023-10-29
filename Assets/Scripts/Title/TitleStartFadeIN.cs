using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleStartFadeIN : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    void Start()
    {
        Fade();
    }
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
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
}
