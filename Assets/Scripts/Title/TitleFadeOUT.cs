using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeOUT : MonoBehaviour
{
    public Image Panel;
    public GameObject game;
    public Slider hp;
    public Slider bossBar;

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

        //Fade OUT
        time = 0f;

        Panel.gameObject.SetActive(true);
        game.SetActive(false);
        hp.gameObject.SetActive(false);
        bossBar.gameObject.SetActive(false);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }

        game.SetActive(true);
        hp.gameObject.SetActive(true);
        bossBar.gameObject.SetActive(true);
        Panel.gameObject.SetActive(false);

        yield return null;
    }
}
