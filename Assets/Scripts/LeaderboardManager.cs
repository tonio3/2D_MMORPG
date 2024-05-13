using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] Transform leaderboardContent;
    [SerializeField] Button _playerButtonPrefab;
    [SerializeField] Button _otherPlayerButtonPrefab;
    private readonly string leaderboardId = "LEADERBOARD"; 
    private List<GameObject> gameObjects = new List<GameObject>();


    private void OnDisable()
    {
        Clear();
    }

    public void Clear()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Destroy(gameObjects[i]);
        }

        gameObjects.Clear();

    }

    private void OnEnable()
    {

        GetScores();
    }

    public async void GetScores()
    {        
        var rangeLimit = 5;
        var scoresResponse = await LeaderboardsService.Instance.GetPlayerRangeAsync(
            leaderboardId,
            new GetPlayerRangeOptions { RangeLimit = rangeLimit }
        );

        foreach (var score in scoresResponse.Results)
        {
            Button newButton = null;

            if(score.PlayerId != AuthenticationService.Instance.PlayerId)
            {
               newButton = Instantiate(_otherPlayerButtonPrefab);
                var button = newButton.GetComponent<LeaderboardOtherPlayerButton>();
                button.playerId = score.PlayerId;
                button.playerName = score.PlayerName;
            }

            else
            {
                 newButton = Instantiate(_playerButtonPrefab);
            }

            newButton.transform.SetParent(leaderboardContent.transform, false);
            gameObjects.Add(newButton.gameObject);
    
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = score.PlayerName + "";
            newButton.gameObject.SetActive(true);
        }
    }
 
}
