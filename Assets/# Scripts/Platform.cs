using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour, IActivatable, IDeactivable
{
    [SerializeField] private bool m_isActivated = true;
    [SerializeField] private bool m_isInitialPosIsFirst = true;
    [SerializeField] private float m_speed = 10f;
    [SerializeField] private Vector3[] m_path;

    private int m_current = -1;

    private void Awake()
    {
        if (m_isInitialPosIsFirst)
        {
            var list = m_path.ToList();
            list.Add(this.transform.position);
            m_path = list.ToArray();
        }
    }

    private void Start()
    {
        if (m_isActivated) Activate();
    }


    public void ToggleActive()
    {
        m_isActivated = !m_isActivated;

        if (m_isActivated) Activate();
        else Deactivate();
    }

    public void Activate()
    {
        m_isActivated = true;
        LoopCallbackRecursive();
    }

    public void Deactivate()
    {
        m_isActivated = false;
        m_current--; // Чтобы при повторной активации объект продолжил движение
        transform.DOMove(this.transform.position, 0); // Остановка на месте
    }


    public void LoopCallbackRecursive()
    {
        if (!m_isActivated) return;

        m_current = (m_current + 1) % m_path.Length;

        float distance = Vector3.Distance(m_path[m_current], this.transform.position);
        float duration = distance / m_speed;

        this.transform
            .DOMove(m_path[m_current], duration)
            .SetEase(Ease.Linear)
            .onComplete += LoopCallbackRecursive;

        // TODO: надеюсь стек не переполнится из-за таких вызовов
    }


    [ContextMenu("Добавить позицию в маршрут")]
    public void AddCurrentPosToPath()
    {
        var list = m_path.ToList();
        list.Add(this.transform.position);
        m_path = list.ToArray();
    }
}
