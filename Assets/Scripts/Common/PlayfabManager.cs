using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField] private LeaderboardRow leaderboardRowPrefab;
    [SerializeField] private Transform leaderboardRowsParent;

    [Space] [SerializeField] private TMP_InputField inputField;

    public string userDisplayName = null;

    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    private void OnSuccess(LoginResult result)
    {
        Debug.Log("Success login/account create!");
    }

    private void OnError(PlayFabError error)
    {
        //Debug.LogWarning("Error while logging in/creating account!");
        Debug.LogWarning(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in leaderboardRowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            var newRow = Instantiate(leaderboardRowPrefab, leaderboardRowsParent);
            //newRow.SetText((item.Position + 1).ToString(), item.PlayFabId, item.StatValue.ToString());
            newRow.SetText((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());

            Debug.Log($"PLACE: {item.Position} | ID: {item.DisplayName} | SCORE: {item.StatValue}");
        }
    }

    public void SetOrUpdateUserDisplayName()
    {
        GameManager.instance.audioManager.PlayClick();
        
        if (inputField.text.Length < 3)
            return;

        string name = inputField.text;

        userDisplayName = name;

        PlayFabClientAPI.UpdateUserTitleDisplayName(
            // Request
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name
            },
            // Success
            (UpdateUserTitleDisplayNameResult result) =>
            {
                Debug.Log("UpdateUserTitleDisplayName completed.");
                GetLeaderboard();
            },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("UpdateUserTitleDisplayName failed.");
                Debug.LogError(error.GenerateErrorReport());
            }
        );
    }
}