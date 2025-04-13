using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private PlayerNullChars[] _playerNullLimits;

    private PlayerController _currentPlayer;


    public static PlayerManager Instance { get; private set; } // TODO: Надеюсь этот позор никто не увидит...
    public PlayerController Player => _currentPlayer;
    public PlayerNullChars[] PlayerNullLimits => _playerNullLimits;
    public UnityEvent NullLimitsUpdatedEvent { get; private set; } = new();


    private void Awake()
    {
        Instance = this;

        _playerNullLimits = _playerNullLimits.Distinct().ToArray();

        NullLimitsUpdatedEvent.AddListener(OnNullLimitsUpdated);
    }

    private void Start()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// Спавнит игрока в указанной точке спавна
    /// </summary>
    public void SpawnPlayer()
    {
        if (_currentPlayer != null)
        {
            Destroy(_currentPlayer);
        }

        Transform spawnPoint = CheckPointManager.Instance._currentCheckPoint.transform;

        if (_playerPrefab != null && spawnPoint != null)
        {
            _currentPlayer = Instantiate(_playerPrefab, spawnPoint.position, spawnPoint.rotation)
                .GetComponent<PlayerController>();

            _currentPlayer.GetComponent<Activator>().HintUI = SceneManager.Instance.hintUI;
            _currentPlayer.OnKilled.AddListener(SpawnPlayer);

            NullLimitsApply(_currentPlayer);
        }
        else
        {
            Debug.LogError("PlayerPrefab or SpawnPoint not assigned in CursorManager!");
        }
    }

    public void AddNullLimit(PlayerNullChars limit)
    {
        if (_playerNullLimits.Contains(limit)) return;

        var list = _playerNullLimits.ToList();
        list.Add(limit);
        _playerNullLimits = list.ToArray();

        NullLimitsUpdatedEvent.Invoke();
    }

    public void RemoveNullLimit(PlayerNullChars limit)
    {
        if (!_playerNullLimits.Contains(limit)) return;

        var list = _playerNullLimits.ToList();
        list.Remove(limit);
        _playerNullLimits = list.ToArray();

        NullLimitsUpdatedEvent.Invoke();
    }

    // Для настройки на сцене
    public void LockJumping(bool state)
    {
        if (_currentPlayer is null) return;
        _currentPlayer._fpsController.JumpingIsLocked = state;
    }
    public void LockSeating(bool state)
    {
        if (_currentPlayer is null) return;
        _currentPlayer._fpsController.SeatingIsLocked = state;
    }
    public void LockMovement(bool state)
    {
        if (_currentPlayer is null) return;
        _currentPlayer._fpsController.MovementIsLocked = state;
    }
    public void LockShooting(bool state)
    {
        if (_currentPlayer is null) return;
        _currentPlayer._gun.ShotingIsLocked = state;
    }


    private void OnNullLimitsUpdated() => NullLimitsApply(_currentPlayer);

    private void NullLimitsApply(PlayerController player)
    {
        if (gameObject is null) return;

        player._gun.ShotingIsLocked = false;
        player._fpsController.JumpingIsLocked = false;
        player._fpsController.SeatingIsLocked = false;
        player._fpsController.MovementIsLocked = false;

        foreach (var limit in _playerNullLimits)
        {
            switch (limit)
            {
                case PlayerNullChars.Jumping:
                    player._fpsController.JumpingIsLocked = true;
                    break;
                case PlayerNullChars.Seating:
                    player._fpsController.SeatingIsLocked = true;
                    break;
                case PlayerNullChars.Movement:
                    player._fpsController.MovementIsLocked = true;
                    break;
                case PlayerNullChars.Shooting:
                    player._gun.ShotingIsLocked = true;
                    break;
            }
        }
    }
}

public enum PlayerNullChars
{
    Jumping,
    Movement,
    Shooting,
    Seating
}