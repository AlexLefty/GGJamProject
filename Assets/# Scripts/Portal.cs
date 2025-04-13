using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour, IActivatable, IDeactivable
{
    [Header("Settings")]
    [SerializeField] private bool isActivated = true;
    [SerializeField] private float timeTeleportation = 3;
    [SerializeField] private Transform position;
    [Header("Callbacks")]
    [SerializeField, Space] private UnityEvent onActivating = new();
    [SerializeField, Space] private UnityEvent onDeactivating = new();
    [SerializeField, Space] private UnityEvent onStartTeleportation = new();
    [Header("Bindings")]
    [SerializeField] private GameObject lightning;

    private bool isTeleporting = false;
    [SerializeField] private PlayerControl player;


    private void Awake()
    {
        lightning?.SetActive(isActivated);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerControl>();
            Teleport();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) player = null;
    }


    public void Activate()
    {
        isActivated = true;
        onActivating.Invoke();
    }
    public void Deactivate()
    {
        isActivated = false;
        onDeactivating.Invoke();
    }


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