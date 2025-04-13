using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float m_shootDistance = 100f;
    [SerializeField] private KeyCode m_shootKey = KeyCode.Mouse0;
    [SerializeField] private float m_damage = 10f;
    [SerializeField] private bool m_ShootingIsLocked = true;

    [Header("Effects")]
    //[SerializeField] private ParticleSystem m_muzzleFlash;
    [SerializeField] private AudioClip m_shootSound;
    //[SerializeField] private GameObject m_hitEffectPrefab;

    private AudioSource _audioSource;
    private Camera _playerCamera;


    public bool ShotingIsLocked { get => m_ShootingIsLocked; set => m_ShootingIsLocked = value; }


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_shootKey) && !m_ShootingIsLocked)
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
        if (activatable != null) print("Activate");
        activatable?.Activate();
    }

    private void CheckForDamageable(Collider target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();

        damageable?.TakeDamage(m_damage);
    }

    private void PlayShootEffects()
    {
        //m_muzzleFlash?.Play();

        _audioSource?.PlayOneShot(m_shootSound);
    }

    private void SpawnHitEffect(Vector3 position, Vector3 normal)
    {
        //if (m_hitEffectPrefab is not null)
        //{
        //    GameObject effect = Instantiate(m_hitEffectPrefab, position, Quaternion.LookRotation(normal));
        //    Destroy(effect, 1f);
        //}
    }
}
