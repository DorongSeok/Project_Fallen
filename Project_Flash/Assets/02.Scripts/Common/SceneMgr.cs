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
    //IEnumerator ReTestButton()
    //{
    //    DataManager.instance.ResetSavePoint();
    //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("H", LoadSceneMode.Single);
    //    while (!asyncOperation.isDone)
    //    {
    //        yield return null;
    //    }
    //}
    public void CoroutineOpenPlayScene()
    {
        StartCoroutine(nameof(OpenPlayScene));
    }
    public void CoroutineOpenMainScene()
    {
        StartCoroutine(nameof(OpenPlayScene));
    }
    public void CoroutineOpenTestScene()
    {
        StartCoroutine(nameof(OpenTestScene));
    }
    //public void CoroutineReTestButton()
    //{
    //    StartCoroutine(nameof(ReTestButton));
    //}
}
