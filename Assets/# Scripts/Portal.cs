using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] private float timeTeleportation = 3;
    [SerializeField] private Vector3 position;
    [SerializeField, Space] private UnityEvent onStartTeleportation = new();

    public void Activate()
    {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        onStartTeleportation.Invoke();

        yield return new WaitForSeconds(timeTeleportation);

        SceneManager.Instance.Player.transform.position = this.position;
    }
}