using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillAmountReducer : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float seconds = 5f;

    private Coroutine currentRoutine;
    private bool isActive = false;

    public float Seconds
    {
        get => seconds;
        set => seconds = Mathf.Max(0.01f, value);
    }

    public void Activate()
    {
        if (targetImage != null && targetImage.type != Image.Type.Filled)
        {
            targetImage.type = Image.Type.Filled;
        }

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        isActive = true;
        targetImage.fillAmount = 1f;
        currentRoutine = StartCoroutine(FillRoutine());
    }

    public void Deactivate()
    {
        isActive = false;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        targetImage.fillAmount = 1f;
    }

    private IEnumerator FillRoutine()
    {
        float timer = 0f;

        while (timer < seconds && isActive)
        {
            timer += Time.deltaTime;
            targetImage.fillAmount = 1f - Mathf.Clamp01(timer / seconds);
            yield return null;
        }

        isActive = false;
    }
}
