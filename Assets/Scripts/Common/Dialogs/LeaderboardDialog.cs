using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardDialog : DialogManager
{
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private GameObject sendingButtons;
    public override void ShowDialog()
    {
        sendingButtons.SetActive(false);
        GameManager.instance.playfabManager.GetLeaderboard();

        base.ShowDialog();
    }

    public void ShowDialog(int score)
    {
        sendingButtons.SetActive(true);
        yourScoreText.text = $"Your score: {score}";
        GameManager.instance.playfabManager.GetLeaderboard();
        base.ShowDialog();
    }
}