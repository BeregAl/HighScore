using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CommonUiManager : MonoBehaviour
{
    [SerializeField] private SceneTransitionManager sceneTransition;
    
    public LeaderboardDialog leaderboardDialog;
    public SettingsDialog settingsDialog;

    private void Awake()
    {
    }

    private void OnGameOver()
    {
        
    }

    public void HideTransitionImage()
    {
        if (sceneTransition)
        {
            sceneTransition.HideImage();
        }
    }
    
    public void AnimateTransitionAndDoAction(Action action)
    {
        if (sceneTransition)
        {
            sceneTransition.ShowImageAndDoAction(action);
        }
        else
        {
            action?.Invoke();
        }
    }
}