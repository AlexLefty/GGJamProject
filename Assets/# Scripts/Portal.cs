using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Portal : MonoBehaviour, IActivatable, IDeactivable
{
    [Header("Settings")]
    [SerializeField] private bool isActivated = true;
    [SerializeField] private float timeTeleportation = 3;
    [SerializeField] private AudioClip m_clip;
    [SerializeField] private Transform position;
    [Header("Callbacks")]
    [SerializeField, Space] private UnityEvent onActivating = new();
    [SerializeField, Space] private UnityEvent onDeactivating = new();
    [SerializeField, Space] private UnityEvent onStartTeleportation = new();
    [Header("Bindings")]
    [SerializeField] private GameObject lightning;

    private bool isTeleporting = false;
    private AudioSource m_audioSource;
    private PlayerController player;


    private void Awake()
    {
        lightning?.SetActive(isActivated);

        m_audioSource = GetComponent<AudioSource>();

        if (m_audioSource is not null)
            m_audioSource.clip = m_clip;
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
            m_audioSource.Play();

            player._fpsController.m_CharacterController.enabled = false;
            Vector3 pos = position.position;
            pos.y += 2f;
            player.transform.position = pos;
            player.transform.rotation = position.rotation;
            player._fpsController.m_CharacterController.enabled = true;
        }

        isTeleporting = false;
    }
}