using UnityEngine;
using TMPro;

public class Activator : MonoBehaviour
{
    [Tooltip("Дистанция взаимодействия")]
    [SerializeField] private float m_interactionDistance = 5f;
    [Tooltip("Клавиша активации")]
    [SerializeField] private KeyCode m_activationKey = KeyCode.E;
    [Tooltip("UI-элемент с текстом подсказки")]
    [SerializeField] private GameObject _hintGUI;
    [SerializeField] private TMP_Text _hintText;

    private Camera _mainCamera;
    private ActivatableObject _currentActivatable;

    public GameObject HintUI
    {
        get => _hintGUI;
        set
        {
            _hintGUI = value;

            if (value is not null) 
                _hintText = value.GetComponent<TMP_Text>() ?? GetComponentInChildren<TMP_Text>();
        }
    }


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
                    ChangeHintText(_currentActivatable.ActivationHintFormat);
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
        if (_hintGUI is null) return;

        _hintGUI.SetActive(true);
        Vector3 worldPosition = target.position + Vector3.up * 1.5f;
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
        _hintGUI.transform.position = screenPosition;
    }

    /// <summary>
    /// Измение текста подсказки
    /// </summary>
    /// <param name="hintText"></param>
    private void ChangeHintText(string hintText)
    {
        if (hintText is null) return;

        _hintText.text = string.Format(hintText, m_activationKey.ToString());
    }

    private void ClearCurrentActivatable()
    {
        if (_currentActivatable != null)
        {
            _currentActivatable.Highlight(false);
            _currentActivatable = null;
            _hintGUI.SetActive(false);
        }
    }
}
