using System.Collections;
using UnityEngine;

/// <summary>
/// Позволяет взаимодействовать с активируемым объектом с помощью клавиши 'E'.
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public class InteractableObject : Activatable
{
    private bool m_isAccess;
    private Coroutine m_loopRoutine;


    private void OnTriggerEnter(Collider other)
    {
        m_isAccess = other.gameObject.tag == "Player";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") m_isAccess = false;
    }

    private void OnMouseEnter()
    {
        if (!m_isAccess) return;

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
        while (m_isAccess)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_action.Invoke();

                StopCoroutine(m_loopRoutine);
                m_loopRoutine = null;
            }

            yield return null;
        }

        m_loopRoutine = null;
    }
}
