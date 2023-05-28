using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EndingSceneManager : MonoBehaviour
{
    private float clearTime;
    private float fallingCount;
    private void Start()
    {
        clearTime = Managers.data.GetSecond();
        fallingCount = Managers.data.GetFallenCount();
        ShowRecord();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.data.ResetSaveGameData();
            StartCoroutine(nameof(Main_UISceneOpen));
        }
    }

    IEnumerator Main_UISceneOpen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void ShowRecord()
    {
        Debug.Log("Falling Count : " + fallingCount);
        Debug.Log(getParseTime(clearTime));
    }
    private string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("hh\\:mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + "h " + tokens[1] + "m " + tokens[2] + "s";
    }
}
