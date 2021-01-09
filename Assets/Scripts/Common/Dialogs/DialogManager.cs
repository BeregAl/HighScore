using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image backImage;
    private Color _initColor;

    private void Awake()
    {
        if (backImage)
        {
            _initColor = backImage.color;
        }
    }

    public virtual void ShowDialog()
    {
        if (backImage)
        {
            DOTween.Kill(backImage);
            backImage.DOFade(0.8f, 0.25f);
        }

        gameObject.SetActive(true);
    }

    public virtual void CloseDialog()
    {
        if (!gameObject.activeSelf) return;
        gameObject.SetActive(false);

        if (backImage)
        {
            DOTween.Kill(backImage);
            backImage.color = _initColor;
        }
    }
}
