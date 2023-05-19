using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject option;
    public GameObject level;
    public EventSystem eventSystem;

    IEnumerator ContinueButtonClick()
    {
        AsyncOperation asyncOperation;
        if (Managers.data.GetLevel() == 1)
        {
            asyncOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
        }
        else
        {
            asyncOperation = null;
        }
        
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator NewGameButtonClick()
    {
        Managers.data.ResetSaveData();
        Managers.data.SetIsFirstPlay(false);
        Managers.data.SetLevel(1);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator CreditButtonClick()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator MoveToMainScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
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
        option.SetActive(true);
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(true);
        eventSystem.sendNavigationEvents = false;
    }
    public void CoroutineCreditButtonClick()
    {
        StartCoroutine(nameof(CreditButtonClick));
    }
    public void ExitButtonClick()
    {
        Application.Quit();
    }
    public void CoroutineMoveToMainScene()
    {
        StartCoroutine(nameof(MoveToMainScene));
    }
    public void OptionExitButtonClick()
    {
        eventSystem.sendNavigationEvents = true;
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(false);
        option.SetActive(false);
    }
}
