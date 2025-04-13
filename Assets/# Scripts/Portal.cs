using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] private bool isActivated = true;
    [SerializeField] private float timeTeleportation = 3;
    [SerializeField] private Transform position;
    [SerializeField, Space] private UnityEvent onStartTeleportation = new();

    private bool isTeleporting = false;
    private PlayerControl player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerControl>();
            Teleport();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) player = null;
    }


    public void Activate() => isActivated = true;

    private async void Teleport()
    {
        if (!isActivated || isTeleporting) return;

        isTeleporting = true;

        onStartTeleportation.Invoke();

        await UniTask.Delay((int)timeTeleportation * 1000);

        if (player is not null)
        {
            player.transform.position = position.position;
            player.transform.rotation = position.rotation;
        }

        isTeleporting = false;
    }
}