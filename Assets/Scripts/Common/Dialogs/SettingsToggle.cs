using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToggle : MonoBehaviour
{
    public Image back;
    public RectTransform circle;

    [SerializeField] private Color32 _inactiveColor = new Color32(217, 216, 216, 255);
    [SerializeField] private Color32 _activeColor = new Color32(255, 166, 178, 255);
    private readonly float _activeCirclePosition = 55f;
    private readonly float _inactiveCirclePosition = 0f;

    public void SetState(bool state)
    {
        var color = state ? _activeColor : _inactiveColor;
        var positionX = state ? _activeCirclePosition : _inactiveCirclePosition;

        DOTween.Kill(back);
        DOTween.Kill(circle);

        back.DOColor(color, 0.25f);
        circle.DOAnchorPosX(positionX, 0.25f);
    }

    public void SetStateImmediate(bool state)
    {
        var color = state ? _activeColor : _inactiveColor;
        var positionX = state ? _activeCirclePosition : _inactiveCirclePosition;

        back.color = color;
        circle.anchoredPosition = new Vector2(positionX, circle.anchoredPosition.y);
    }
}