using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Базовый класс для всех активируемых предметов. Нужно унаследовать его и добавить логику активации.
/// </summary>
public abstract class Activatable : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] protected bool m_isMultipleUse;
    [Tooltip("Не работает если находится в группе кнопок"), Space]
    [SerializeField] protected UnityEvent m_action = new();
    [Header("Bindings")]
    [SerializeField] private ActivatableGroup group;

    private bool m_isActivated;


    public bool IsActivated
    {
        get => m_isActivated;
        internal set => m_isActivated = value;
    }


    protected virtual void Awake()
    {
        if (group is not null)
            group._activators.Add(this);
    }

    private void OnDestroy()
    {
        if (group is not null && group._activators.Contains(this))
            group._activators.Remove(this);
    }

    public void Activate()
    {
        if (group is not null)
        {
            m_isActivated = true;
            group.Activate();
        }
        else if (m_isMultipleUse)
        {
            m_action.Invoke();
        }
        else if (!m_isActivated)
        {
            m_action.Invoke();
            m_isActivated = true;
        }
    }
}
