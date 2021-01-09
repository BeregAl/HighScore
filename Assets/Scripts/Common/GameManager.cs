using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found!");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    public PlayfabManager playfabManager;
    public CommonUiManager uiManager;

    private void Start()
    {
#if !UNITY_EDITOR
        LoadFirstScene();
#endif
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}