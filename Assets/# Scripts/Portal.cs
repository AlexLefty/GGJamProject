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
    [SerializeField] private PlayerController player;


    private void Awake()
    {
        lightning?.SetActive(isActivated);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
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
            player._fpsController.m_CharacterController.enabled = false;
            player.transform.position = position.position;
            player.transform.rotation = position.rotation;
            player._fpsController.m_CharacterController.enabled = true;
        }

        isTeleporting = false;
    }
}