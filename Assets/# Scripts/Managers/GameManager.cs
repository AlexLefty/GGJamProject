using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI settings")]
    public GameObject hintUI;


    public static GameManager Instance { get; private set; } // TODO: Надеюсь этот позор никто не увидит...


    private void Awake()
    {
        HideCursor();

        Instance = this;
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
