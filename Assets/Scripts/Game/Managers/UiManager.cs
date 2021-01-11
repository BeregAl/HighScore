using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UiManager : MonoBehaviour, IManager
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text scoreMultiplierText;

        [SerializeField] private PopupText popupText;

        public void SetScore(float score)
        {
            scoreText.text = "Score: " + score;
        }

        public void SetScoreMultiplier(int multiplier)
        {
            if (multiplier < 2)
                return;

            scoreMultiplierText.text = "x" + multiplier;
        }

        public PopupText CreatePopupText(Transform parent)
        {
            var pos = parent.transform.position;
            pos.y += 0.5f;
            var newPopup = Instantiate(popupText, pos, Quaternion.identity);
            return newPopup;
        }

        public void Subscribe()
        {
        }

        public void Unsubscribe()
        {
        }
    }
}