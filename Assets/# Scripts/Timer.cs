using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour, IActivatable, IDeactivable
{
    [SerializeField] private float m_time;
    [SerializeField, Space] private UnityEvent onTimerEnd;

    private Coroutine timerCoroutine;

    public void Activate()
    {
        if (timerCoroutine != null) return;
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    public void Deactivate()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private System.Collections.IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(m_time);
        onTimerEnd?.Invoke();
        timerCoroutine = null;
    }
}
