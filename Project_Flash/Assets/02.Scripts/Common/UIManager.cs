using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject option;
    IEnumerator ContinueButtonClick()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Play", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator NewGameButtonClick()
    {
        DataManager.instance.ResetSaveData();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Play", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator CreditButtonClick()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Credit", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator MoveToMainScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
    
    public void CoroutineContinueButtonClick()
    {
        Debug.Log("Continue");
        StartCoroutine(nameof(ContinueButtonClick));
    }
    public void CoroutineNewGameButtonClick()
    {
        Debug.Log("NewGame");
        StartCoroutine(nameof(NewGameButtonClick));
    }
    public void OptionButtonClick()
    {
        Debug.Log("Option");
        option.SetActive(true);
    }
    public void CoroutineCreditButtonClick()
    {
        Debug.Log("Credit");
        StartCoroutine(nameof(CreditButtonClick));
    }
    public void ExitButtonClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void CoroutineMoveToMainScene()
    {
        Debug.Log("MainScene");
        StartCoroutine(nameof(MoveToMainScene));
    }
    public void OptionExitButtonClick()
    {
        option.SetActive(false);
    }
}
