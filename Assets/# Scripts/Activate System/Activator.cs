using UnityEngine;
using TMPro;

public class Activator : MonoBehaviour
{
    [Tooltip("Дистанция взаимодействия")]
    [SerializeField] private float m_interactionDistance = 5f;
    [Tooltip("Клавиша активации")]
    [SerializeField] private KeyCode m_activationKey = KeyCode.E;
    [Tooltip("UI-элемент с текстом подсказки")]
    [SerializeField] private GameObject _interactionHintUI;
    [SerializeField] private TMP_Text _hintText;

    private Camera _mainCamera;
    private ActivatableObject _currentActivatable;


    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        CheckForActivatable();
    }

    private void Update()
    {
        if (_currentActivatable is null) return;

        if (Input.GetKeyDown(m_activationKey))
        {
            _currentActivatable.Activate();
        }
    }

    private void CheckForActivatable()
    {
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_interactionDistance))
        {
            var activatable = hit.collider.GetComponent<ActivatableObject>();

            if (activatable != null)
            {
                if (_currentActivatable != activatable)
                {
                    ClearCurrentActivatable();
                    _currentActivatable = activatable;

                    PositionHintUI(hit.collider.transform);
                    _hintText.text = string.Format(_currentActivatable.ActivationHintFormat, m_activationKey.ToString());
                }
            }
            else
            {
                ClearCurrentActivatable();
            }
        }
        else
        {
            ClearCurrentActivatable();
        }
    }

    /// <summary>
    /// Ставит подсказку над объектом
    /// </summary>
    /// <param name="target"></param>
    private void PositionHintUI(Transform target)
    {
        if (_interactionHintUI == null) return;

        _interactionHintUI.SetActive(true);
        Vector3 worldPosition = target.position + Vector3.up * 1.5f;
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
        _interactionHintUI.transform.position = screenPosition;
    }

    private void ClearCurrentActivatable()
    {
        if (_currentActivatable != null)
        {
            _currentActivatable.Highlight(false);
            _currentActivatable = null;
            _interactionHintUI.SetActive(false);
        }
    }
}
