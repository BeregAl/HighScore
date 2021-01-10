using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        GameManager.instance.audioManager.PlayClick();
        GameManager.instance.uiManager.AnimateTransitionAndDoAction(() =>
        {
            SceneManager.LoadScene("Game");
            GameManager.instance.uiManager.HideTransitionImage();
        });
    }

    public void OpenLeaderboard()
    {
        GameManager.instance.audioManager.PlayClick();
        GameManager.instance.uiManager.leaderboardDialog.ShowDialog();
    }
    
    public void OpenSettings()
    {
        GameManager.instance.audioManager.PlayClick();
        GameManager.instance.uiManager.settingsDialog.ShowDialog();
    }
}