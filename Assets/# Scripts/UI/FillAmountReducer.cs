using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FillAmountReducer : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float seconds = 5f;

    private bool isActive = false;
    private float timer = 0f;

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

        isActive = true;
        timer = 0f;

        if (targetImage != null)
            targetImage.fillAmount = 1f;

        TimerLoop();
    }

    public void Deactivate()
    {
        isActive = false;
        targetImage.fillAmount = 1f;
    }

    private async void TimerLoop()
    {
        if (!isActive || targetImage == null) return;

        var delay = UniTask.Delay((int)Time.fixedDeltaTime*1000);
        float progress = 0f;

        while (progress < 1f)
        {
            timer += Time.fixedDeltaTime;
            progress = Mathf.Clamp01(timer / seconds);
            await delay;
        }

        isActive = false;
    }
}

