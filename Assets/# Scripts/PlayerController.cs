using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(FirstPersonController))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Space] private UnityEvent _onKilled;

    [Header("Bindings")]
    public Gun _gun; // TODO: Надеюсь этот позор никто не увидит...

    [HideInInspector] public FirstPersonController _fpsController;


    public UnityEvent OnKilled => _onKilled;


    private void Awake()
    {
        _fpsController = gameObject.GetComponent<FirstPersonController>();
    }

    public void Kill()
    {
        OnKilled.Invoke();
    }
}
