using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoToMenuScene()
    {
        GameManager.instance.audioManager.PlayClick();
        GameManager.instance.uiManager.AnimateTransitionAndDoAction(() =>
        {
            SceneManager.LoadScene("Menu");
            GameManager.instance.uiManager.HideTransitionImage();
        });
    }
}
