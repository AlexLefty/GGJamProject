using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI settings")]
    public GameObject hintUI;

    private bool m_isPaused;

    public static GameManager Instance { get; private set; } // TODO: Надеюсь этот позор никто не увидит...
    public UnityEvent GameResumed { get; private set; } = new();
    public UnityEvent GamePaused { get; private set; } = new();


    private void Awake()
    {
        HideCursor();

        Instance = this;
    }
    


    public void Resume()
    {
        if (!m_isPaused) return;

        ShowCursor();
        Time.timeScale = 1f;
        m_isPaused = false;
        GameResumed.Invoke();
    }

    public void Pause()
    {
        if (m_isPaused) return;

        ShowCursor();
        Time.timeScale = 0f;
        m_isPaused = true;
        GamePaused.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1"); // TODO: временное решение
        Resume();
    }

    public void Exit()
    {
        Application.Quit(); // Или переход на главное меню
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
}
