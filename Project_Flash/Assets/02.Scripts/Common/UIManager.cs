using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
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
        StartCoroutine(nameof(ContinueButtonClick));
    }
    public void CoroutineNewGameButtonClick()
    {
        StartCoroutine(nameof(NewGameButtonClick));
    }
    public void OptionButtonClick()
    {

    }
    public void CoroutineCreditButtonClick()
    {
        StartCoroutine(nameof(CreditButtonClick));
    }
    public void ExitButtonClick()
    {

    }
    public void CoroutineMoveToMainScene()
    {
        StartCoroutine(nameof(MoveToMainScene));
    }

}
