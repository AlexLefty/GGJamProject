using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public CheckPoint _currentCheckPoint;

    public static CheckPointManager Instance { get; private set; } // TODO: Надеюсь этот позор никто не увидит...

    private void Awake()
    {
        Instance = this;
    }

    public void Swap(CheckPoint point)
    {
        _currentCheckPoint = point;
    }
}
