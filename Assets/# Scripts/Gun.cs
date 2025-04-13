using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float m_shootDistance = 100f;
    [SerializeField] private KeyCode m_shootKey = KeyCode.Mouse0;
    [SerializeField] private LayerMask m_layerMask_E;
    [SerializeField] private float m_delayShoots = 1.5f;
    [SerializeField] private float m_damage = 10f;
    [SerializeField] private bool m_ShootingIsLocked = true;

    [Header("Effects")]
    //[SerializeField] private ParticleSystem m_muzzleFlash;
    [SerializeField] private AudioClip m_shootSound;
    [SerializeField] private GameObject m_hitEffectPrefab;

    private bool m_isReloaded;
    private AudioSource _audioSource;
    private Animator _animator;
    private Camera _playerCamera;


    public bool ShotingIsLocked { get => m_ShootingIsLocked; set => m_ShootingIsLocked = value; }


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_shootKey) && !m_ShootingIsLocked && !m_isReloaded)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Delay();

        PlayShootEffects();

        Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit, hitE;

        if (Physics.Raycast(ray, out hitE, m_shootDistance, m_layerMask_E))
        {
            CheckForActivatable(hitE.collider);
        }

        if (Physics.Raycast(ray, out hit, m_shootDistance))
        {
            SpawnHitEffect(hit.point, hit.normal);

            CheckForDamageable(hit.collider);
        }
    }

    private async void Delay()
    {
        m_isReloaded = true;
        await UniTask.Delay((int)m_delayShoots*1000);
        m_isReloaded = false;
    }

    private void CheckForActivatable(Collider target)
    {
        IActivatable activatable = target.GetComponent<IActivatable>();

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

        _animator.SetTrigger("shoot");
        _audioSource?.PlayOneShot(m_shootSound);
    }

    private void SpawnHitEffect(Vector3 position, Vector3 normal)
    {
        if (m_hitEffectPrefab is not null)
        {
            GameObject effect = Instantiate(m_hitEffectPrefab, position, Quaternion.LookRotation(normal));
            Destroy(effect, 1f);
        }
    }
}
