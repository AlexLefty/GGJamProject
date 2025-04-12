using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float m_shootDistance = 100f;
    [SerializeField] private KeyCode m_shootKey = KeyCode.Mouse0;
    [SerializeField] private float m_damage = 10f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem m_muzzleFlash;
    [SerializeField] private AudioClip m_shootSound;
    [SerializeField] private GameObject m_hitEffectPrefab;

    private AudioSource _audioSource;
    private Camera _playerCamera;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_shootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayShootEffects();

        Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_shootDistance))
        {
            SpawnHitEffect(hit.point, hit.normal);

            CheckForActivatable(hit.collider);
            CheckForDamageable(hit.collider);
        }
    }

    private void CheckForActivatable(Collider target)
    {
        IActivatable activatable = target.GetComponent<IActivatable>();
        if (activatable != null)
        {
            activatable.Activate();
        }
    }

    private void CheckForDamageable(Collider target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(m_damage);
        }
    }

    private void PlayShootEffects()
    {
        if (m_muzzleFlash != null)
            m_muzzleFlash.Play();

        if (m_shootSound != null)
            _audioSource.PlayOneShot(m_shootSound);
    }

    private void SpawnHitEffect(Vector3 position, Vector3 normal)
    {
        if (m_hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(m_hitEffectPrefab, position, Quaternion.LookRotation(normal));
            Destroy(effect, 1f);
        }
    }
}
