using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent action;

    private Coroutine m_loopRoutine;

    private void OnMouseEnter()
    {
        m_loopRoutine = StartCoroutine(Loop());
    }

    private void OnMouseExit()
    {
        if (m_loopRoutine is not null)
        {
            StopCoroutine(m_loopRoutine);
            m_loopRoutine = null;
        }
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                action.Invoke();

                StopCoroutine(m_loopRoutine);
                m_loopRoutine = null;
            }

            yield return null;
        }
    }
}
