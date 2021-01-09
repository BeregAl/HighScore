using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardDialog : DialogManager
{
    public override void ShowDialog()
    {
        GameManager.instance.playfabManager.GetLeaderboard();

        base.ShowDialog();
    }
}