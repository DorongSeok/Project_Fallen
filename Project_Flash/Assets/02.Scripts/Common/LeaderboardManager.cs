using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks.Data;
using Steamworks;

public class LeaderboardManager : MonoBehaviour
{
    private UIManager sceneManager;

    Leaderboard lb;
    void Start()
    {
        sceneManager = this.GetComponent<UIManager>();
        DisplayLeaderboard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.CoroutineMoveToMainScene();
        }
    }
    async void DisplayLeaderboard()
    {
        try
        {
            var leaderboard = await SteamUserStats.FindLeaderboardAsync("Leaderboard");
            lb = (Leaderboard)leaderboard;

            var globalScores = await lb.GetScoresAsync(20);
            foreach (var item in globalScores)
            {
                Debug.Log($"{item.GlobalRank}: {item.Score} {item.User}");
            }

            var surroundScores = await lb.GetScoresAroundUserAsync(-10, 10);
            foreach (var item in surroundScores)
            {
                Debug.Log($"{item.GlobalRank}: {item.Score} {item.User}");
            }
        }
        catch 
        {

        }
        
    }
}
