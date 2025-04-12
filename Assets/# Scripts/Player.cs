using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent onKilled;

    public UnityEvent OnKilled => onKilled;


    public void Kill()
    {
        OnKilled.Invoke();

        Destroy(gameObject);
    }
}
