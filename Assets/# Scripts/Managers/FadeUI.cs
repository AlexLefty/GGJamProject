using DG.Tweening;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float appearDuration = 2.2f;

    void Start()
    {
        canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, appearDuration).SetEase(Ease.InOutSine);
    }
}
