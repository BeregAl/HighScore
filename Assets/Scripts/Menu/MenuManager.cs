using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        GameManager.instance.uiManager.AnimateTransitionAndDoAction(() =>
        {
            SceneManager.LoadScene("Game");
            GameManager.instance.uiManager.HideTransitionImage();
        });
    }

    public void OpenLeaderboard()
    {
        GameManager.instance.uiManager.leaderboardDialog.ShowDialog();
    }
}