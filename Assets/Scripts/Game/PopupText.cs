using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetText(string value)
    {
        _text.text = value;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        transform.DOScale(Vector3.one, 0.5f);
        Vector3 pos = transform.position;
        pos.y += 0.5f;

        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            pos.x += 0.5f;
        }
        else
        {
            pos.x -= 0.5f;
        }

        transform.DOMove(pos, 0.5f);
        _text.DOFade(0f, 0.5f).SetDelay(1f).OnComplete(() => { Destroy(gameObject); });
    }
}