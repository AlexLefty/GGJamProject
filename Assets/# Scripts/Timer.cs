using System.Timers;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour, IActivatable, IDeactivable
{
    [SerializeField] private float m_time;
    [SerializeField, Space] private UnityEvent onTimerEnd;

    private System.Timers.Timer m_timer;


    public void Activate()
    {
        m_timer = new(m_time);
        m_timer.Elapsed += TimerCallback;
        m_timer.AutoReset = false;
        m_timer.Enabled = true;

    }

    public void Deactivate()
    {
        m_timer.Enabled = false;
        m_timer = null;
    }

    private void TimerCallback(object sender, ElapsedEventArgs eventArgs) => onTimerEnd?.Invoke();
}
