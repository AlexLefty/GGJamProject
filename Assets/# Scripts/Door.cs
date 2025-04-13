using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour, ITogglable
{
    [SerializeField] private bool m_isOpened;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (m_isOpened) Open();
        else Close();
    }

    public void Open()
    {
        _animator.SetBool("isOpen", true);
    }

    public void Close()
    {
        _animator.SetBool("isOpen", false);
    }

    public void Toogle()
    {
        m_isOpened = !m_isOpened;

        if (m_isOpened) Open();
        else Close();
    }
}
