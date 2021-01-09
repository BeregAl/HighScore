using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        SceneManager.LoadScene("Game");
    }

    public void OpenLeaderboard()
    {
        GameManager.instance.uiManager.leaderboardDialog.ShowDialog();
    }
}
