using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float imageShowingTime = 0.5f;
    [SerializeField] private float imageHidingTime = 1f;
    [SerializeField] private float imageHidingDelay = 0f;

    private bool _isHiding;

    internal void ShowImageImmediate()
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.color = Color.white;
        transitionImage.raycastTarget = true;
    }

    internal void ShowImageAndDoAction(Action action)
    {
        DOTween.Kill(transitionImage);
        var imageColor = transitionImage.color;

        transitionImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        transitionImage.raycastTarget = true;
        transitionImage.gameObject.SetActive(true);

        transitionImage.DOFade(1f, imageShowingTime)
            .OnComplete(() => { action?.Invoke(); });
    }

    internal void HideImage()
    {
        if (_isHiding)
        {
            return;
        }

        ForceHideImage();
    }

    internal void ForceHideImage()
    {
        _isHiding = true;
        DOTween.Kill(transitionImage);

        transitionImage.DOFade(0f, imageHidingTime)
            .SetDelay(imageHidingDelay)
            .OnComplete(() =>
            {
                transitionImage.raycastTarget = false;
                transitionImage.gameObject.SetActive(false);
                _isHiding = false;
            })
            .OnKill(() =>
            {
                transitionImage.raycastTarget = false;
                transitionImage.gameObject.SetActive(false);
                _isHiding = false;

                var imageColor = transitionImage.color;
                transitionImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
            });
    }
}