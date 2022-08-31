using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup _curtain;
    [SerializeField] private float _fadeSpeed = 0.03f;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = 1;
    }

    public void Hide() =>
        StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= _fadeSpeed;
            yield return new WaitForSeconds(_fadeSpeed);
        }

        gameObject.SetActive(true);
    }
}
