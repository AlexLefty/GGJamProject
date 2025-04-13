using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuContoller : MonoBehaviour
{
    [SerializeField] private string splashScreenName = "SplashScreen";
    [SerializeField] private string levelName = "Level";


    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void Play()
    {
        SceneManager.LoadScene(splashScreenName, LoadSceneMode.Additive);
        LoadGame();
    }

    public void Exit()
    {
        Application.Quit();
    }

    private async void LoadGame()
    {
        await UniTask.DelayFrame(1);
        await SceneManager.LoadSceneAsync(levelName).ToUniTask();
    }
}
