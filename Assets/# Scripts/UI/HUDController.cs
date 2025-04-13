using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Settings")]
    [Header("Bindings")]
    [SerializeField] private Image imageO2Indicator;
    [SerializeField] private Image imageO2BarIndicator;
    [SerializeField] private Image imageGunIndicator;
    [SerializeField] private Image imageGunBarIndicator;
    [SerializeField] private Image imagePlayerIndicator;
    [SerializeField] private TMP_Text textPersonDialog;

    [Header("Gun references")]
    [SerializeField] private Sprite _sprite_gun_bar_blue;
    [SerializeField] private Sprite _sprite_gun_bar_orange;
    [SerializeField] private Sprite _sprite_gun_blue;
    [SerializeField] private Sprite _sprite_gun_orange;
    [Header("O2 references")]
    [SerializeField] private Sprite _sprite_O2_blue;
    [SerializeField] private Sprite _sprite_O2_orange;
    [SerializeField] private Sprite _sprite_O2bar_blue;
    [SerializeField] private Sprite _sprite_O2bar_orange;
    [Header("Human references")]
    [SerializeField] private Sprite _sprite_Human_blue;
    [SerializeField] private Sprite _sprite_Human_jumpingLock;
    [SerializeField] private Sprite _sprite_Human_seatingLock;
    [SerializeField] private Sprite _sprite_Human_movementLock;



    private void Start()
    {
        PlayerManager.Instance.NullLimitsUpdatedEvent.AddListener(UpdateIndicators);
    }

    private void UpdateIndicators()
    {
        PlayerNullChars[] limits = PlayerManager.Instance.PlayerNullLimits;

        textPersonDialog.text = string.Empty;

        if (limits is null || limits.Length == 0) return;

        string message = "Нулевые характеристики:";

        imageO2Indicator.sprite = _sprite_O2_blue;
        imageO2BarIndicator.sprite = _sprite_O2bar_blue;
        imageGunIndicator.sprite = _sprite_gun_blue;
        imageGunBarIndicator.sprite = _sprite_gun_bar_blue;
        imagePlayerIndicator.sprite = _sprite_Human_blue;

        foreach (var limit in limits)
        {
            switch (limit)
            {
                case PlayerNullChars.Jumping:
                    imagePlayerIndicator.sprite = _sprite_Human_jumpingLock;
                    message += "\r\n- прыжки";
                    break;
                case PlayerNullChars.Seating:
                    imagePlayerIndicator.sprite = _sprite_Human_seatingLock;
                    message += "\r\n- приседания";
                    break;
                case PlayerNullChars.Movement:
                    imagePlayerIndicator.sprite = _sprite_Human_movementLock;
                    message += "\r\n- передвижение";
                    break;
                case PlayerNullChars.Shooting:
                    imageGunIndicator.sprite = _sprite_gun_orange;
                    imageGunBarIndicator.sprite = _sprite_gun_bar_orange;
                    message += "\r\n- выстрелы";
                    break;
                case PlayerNullChars.Breathe:
                    imageO2Indicator.sprite = _sprite_O2_orange;
                    imageO2BarIndicator.sprite = _sprite_O2bar_orange;
                    message += "\r\n- дыхание";
                    break;
            }
        }

        textPersonDialog.text = message;
    }
}
