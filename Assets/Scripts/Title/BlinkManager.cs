using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BlinkManager : MonoBehaviour
{
    public LoopType loopType;
    public TextMeshProUGUI text;
    void Start()
    {
        text.DOFade(0.0f, 1).SetLoops(-1, loopType);
    }
}
