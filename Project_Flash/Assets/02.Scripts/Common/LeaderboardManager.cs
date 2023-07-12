using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks.Data;
using Steamworks;
using UnityEngine.UI;
using System;

public class LeaderboardManager : MonoBehaviour
{
    private UIManager sceneManager;

    public List<GameObject> list_Top10 = new List<GameObject>();
    public List<GameObject> list_MyRank = new List<GameObject>();

    Leaderboard lb;
    void Start()
    {
        //Managers.Sound.Clear();
        //Managers.Sound.Play("BGM/LeaderboardScene_BGM", Define.Sound.Bgm);
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

            var globalScores = await lb.GetScoresAsync(10);
            for (int i = 0; i < globalScores.Length; i++)
            {
                list_Top10[i].GetComponentsInChildren<Text>()[0].text = $"{globalScores[i].GlobalRank}";

                string userName;
                userName = globalScores[i].User.Name;
                if (userName.Length > 10)
                {
                    userName = userName.Substring(0, 10) + "..";
                }
                list_Top10[i].GetComponentsInChildren<Text>()[1].text = userName;

                list_Top10[i].GetComponentsInChildren<Text>()[2].text = $"{getParseTime(globalScores[i].Score)}";
            }

            var surroundScores = await lb.GetScoresAroundUserAsync(-5, 5);
            for (int i = 0; i < surroundScores.Length; i++)
            {
                list_MyRank[i].GetComponentsInChildren<Text>()[0].text = $"{surroundScores[i].GlobalRank}";

                string userName;
                userName = surroundScores[i].User.Name;
                if (userName.Length > 10)
                {
                    userName = userName.Substring(0, 10) + "..";
                }
                list_MyRank[i].GetComponentsInChildren<Text>()[1].text = userName;

                list_MyRank[i].GetComponentsInChildren<Text>()[2].text = $"{getParseTime(surroundScores[i].Score)}";
            }
        }
        catch 
        {

        }
    }
    private string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("hh\\:mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + ":" + tokens[1] + ":" + tokens[2];
    }
}
