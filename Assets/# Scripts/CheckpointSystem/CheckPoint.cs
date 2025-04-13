using UnityEngine;

public class CheckPoint : MonoBehaviour, IActivatable
{
    public void Activate()
    {
        CheckPointManager.Instance.Swap(this);
    }
}
