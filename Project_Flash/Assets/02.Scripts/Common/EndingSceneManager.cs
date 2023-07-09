using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using Steamworks;

public class EndingSceneManager : MonoBehaviour
{
    public GameObject result;
    public GameObject endingCredit;
    public List<Image> endingCreditList = new List<Image>();
    public Image curtain;
    public Text text_PlayTime;
    public Text text_FallingCount;
    public EndingCameraMove endingCameraMove;
    public EndingCharacterMove endingCharacterMove;

    public float fadeSpeed = 0.01f;
    private float fadeCount;

    private float clearTime;
    private float fallingCount;

    Steamworks.Data.Leaderboard lb;
    private void Start()
    {
        Managers.Sound.Clear();
        clearTime = Managers.data.GetSecond();
        fallingCount = Managers.data.GetFallenCount();
        FindLeaderboardAndSetScore();
        EndingCreditStart();
    }
    async void FindLeaderboardAndSetScore()
    {
        try
        {
            var leaderboard = await SteamUserStats.FindLeaderboardAsync("Leaderboard");
            lb = (Steamworks.Data.Leaderboard)leaderboard;
            var result = await lb.SubmitScoreAsync((int)clearTime);
        }
        catch
        {
            
        }
    }
    private void EndingCreditStart()
    {
        endingCredit.SetActive(true);
        StartCoroutine(nameof(EndingCreditCoroutine));
    }
    IEnumerator EndingCreditCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        endingCharacterMove.SetIsMove(true);

        fadeCount = 1.0f;
        while (fadeCount > 0.0f)
        {
            fadeCount -= (fadeSpeed * 0.5f);
            yield return new WaitForSeconds(fadeSpeed * 0.5f);
            curtain.color = new Color(0, 0, 0, fadeCount);
        }
        curtain.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        endingCameraMove.SetIsCamUpTrue();
        Managers.Sound.Play("BGM/EndingScene_BGM", Define.Sound.Bgm);
        yield return new WaitForSeconds(2.0f);

        fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[0].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[0].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[1].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[1].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[2].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[2].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[3].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[3].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[4].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[4].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[5].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(2.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[5].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;

        yield return new WaitForSeconds(1.5f);

        endingCredit.SetActive(false);
        endingCameraMove.SetIsCamDownTrue();

        yield return new WaitForSeconds(3.0f);

        curtain.gameObject.SetActive(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += (fadeSpeed * 0.5f);
            yield return new WaitForSeconds(fadeSpeed * 0.5f);
            curtain.color = new Color(0, 0, 0, fadeCount);
        }

        yield return new WaitForSeconds(0.5f);
        endingCharacterMove.SetIsMove(false);
        ShowResult();
    }
    private void ShowResult()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        text_PlayTime.text = "Play Time\n" + "<color=#00ff00>" + getParseTime(clearTime) + "</color>";
        text_FallingCount.text = "Number of Falling\n" + "<color=#00ff00>" + fallingCount.ToString() + "</color>";
        result.SetActive(true);
    }
    public void GameClear()
    {
        FindLeaderboardAndSetScore();
        Managers.data.ResetSaveGameData();
        StartCoroutine(nameof(Main_UISceneOpen));
    }

    IEnumerator Main_UISceneOpen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("hh\\:mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + "h " + tokens[1] + "m " + tokens[2] + "s";
    }
}
