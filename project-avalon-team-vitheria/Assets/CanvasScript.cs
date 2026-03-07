using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 0f;

    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
    }
}