using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UiManager : MonoBehaviour, IManager
    {
        [SerializeField] private TMP_Text scoreText;

        public void SetScore(float score)
        {
            scoreText.text = "Score: " + score;
        }

        public void Subscribe()
        {
        }

        public void Unsubscribe()
        {
        }
    }
}