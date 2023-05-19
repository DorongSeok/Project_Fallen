using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_CutSceneManager : MonoBehaviour
{
    IEnumerator CheckSceneCoroutine;
    void Start()
    {
        StartCheckSceneEnd();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCheckSceneEnd();
            StartCoroutine(nameof(Main_UISceneOpen));
        }
    }
    private void StartCheckSceneEnd()
    {
        CheckSceneCoroutine = CheckCutSceneEnd();
        StartCoroutine(CheckSceneCoroutine);
    }
    private void StopCheckSceneEnd()
    {
        if (CheckSceneCoroutine != null)
        {
            StopCoroutine(CheckSceneCoroutine);
        }
    }
    IEnumerator CheckCutSceneEnd()
    {
        yield return new WaitForSeconds(2.0f);

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
}
