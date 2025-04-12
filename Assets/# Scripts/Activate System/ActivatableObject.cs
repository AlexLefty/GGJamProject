using UnityEngine;
using UnityEngine.Events;

public class ActivatableObject : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] protected bool m_isMultipleUse;
    [SerializeField] private string m_activationHintFormat = "Нажмите {0} чтобы открыть";
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
    public string ActivationHintFormat => m_activationHintFormat;


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

    /// <summary>
    /// Подсвечивает объект при наведении
    /// </summary>
    /// <param name="state"></param>
    public void Highlight(bool state)
    {

    }
}
