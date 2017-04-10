using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
    TransitionController transitionController;
    public float showDuration = .5f;
    public float hideDuration = .5f;

    public Action OnClick;
    private bool visible;

    void Awake()
    {
        transitionController = GetComponent<TransitionController>();
    }

	public void Show(Action callback = null)
    {
        gameObject.SetActive(true);
        transitionController.Transition(TransitionController.TransitionType.FadeIn, showDuration, callback);
        visible = true;
    }

    public void Hide(Action callback = null)
    {
        transitionController.Transition(TransitionController.TransitionType.FadeOut, showDuration, callback);
        visible = false;
    }

    public void Update()
    {
        if (!visible)
            return;

        if (Input.GetMouseButtonDown(0) && OnClick != null)
            OnClick();
    }
}
