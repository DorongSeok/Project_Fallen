using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    IEnumerator OpenPlayScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Play", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator OpenMainScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator OpenTestScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("H", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void CorutineOpenPlayScene()
    {
        StartCoroutine(nameof(OpenPlayScene));
    }
    public void CorutineOpenMainScene()
    {
        StartCoroutine(nameof(OpenPlayScene));
    }
    public void CorutineOpenTestScene()
    {
        StartCoroutine(nameof(OpenTestScene));
    }
}
