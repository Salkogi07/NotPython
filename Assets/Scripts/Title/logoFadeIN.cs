using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logoFadeIN : MonoBehaviour
{
    public Image Panel;
    public Image title;
    float time = 0f;
    float F_time = 1f;

    void Start()
    {
        Fade();
    }
    private void Fade()
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

        yield return new WaitForSeconds(1f);

        //Fade OUT
        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        yield return null;
    }
}
