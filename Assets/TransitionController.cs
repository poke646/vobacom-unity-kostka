using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {
    public bool startVisible = false;

    CanvasRenderer[] crs;

    public enum TransitionType
    {
        FadeIn,
        FadeOut
    };

    TransitionType type;
    float duration = .5f;
    bool animate = false;
    float startTime, elapsedTime;
    Action transitionFinished;
    float t;

    private void Awake()
    {
        crs = GetComponentsInChildren<CanvasRenderer>();

        if (!startVisible)
        {
            foreach(CanvasRenderer cr in crs)
                cr.SetAlpha(0f);
        }
    }

    private void Animate()
    {
        if (!animate)
            return;

        float alpha;
        if (type == TransitionType.FadeIn)
            alpha = Mathf.Lerp(0, 1, t);
        else
            alpha = Mathf.Lerp(1, 0, t);

        for (int i = 0; i < crs.Length; i++)
        {
            crs[i].SetAlpha(alpha);
        }

        if (t < 1)
        {
            t += Time.deltaTime / duration;
        }
        else
        {
            animate = false;
            if (transitionFinished != null)
                transitionFinished();
        }
    }

    public void Update()
    {
        Animate();
    }

    public void Transition(TransitionType type, float duration, Action callback = null)
    {
        this.type = type;
        this.duration = duration;
        animate = true;
        t = 0f;
        transitionFinished = callback;
    }
}
