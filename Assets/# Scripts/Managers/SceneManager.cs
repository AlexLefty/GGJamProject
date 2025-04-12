using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;


    private GameObject currentPlayer;

    private void Awake()
    {
        HideCursor();
        SpawnPlayer();
    }

    /// <summary>
    /// Скрывает и замораживает курсор, устанавливая его в центр экрана
    /// </summary>
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Показывает и разблокирует курсор
    /// </summary>
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Спавнит игрока в указанной точке спавна
    /// </summary>
    public void SpawnPlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        if (playerPrefab != null && spawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("PlayerPrefab or SpawnPoint not assigned in CursorManager!");
        }
    }
}
