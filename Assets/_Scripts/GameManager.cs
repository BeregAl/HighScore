using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

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

    [SerializeField] private PlayfabManager playfabManager;
    [SerializeField] private UiManager uiManager;
}
