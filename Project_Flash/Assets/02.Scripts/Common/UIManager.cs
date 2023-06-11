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
    public GameObject warning;
    public GameObject panel;

    private AsyncOperation asyncOperation;

    IEnumerator ContinueButtonClick()
    {
        if (asyncOperation == null)
        {
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
    }
    IEnumerator NewGameStart()
    {
        if (asyncOperation == null)
        {
            Managers.data.ResetSaveGameData();
            Managers.data.SetIsFirstPlay(false);
            Managers.data.SetLevel(1);
            asyncOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
    IEnumerator CreditButtonClick()
    {
        if (asyncOperation == null)
        {
            asyncOperation = SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
    IEnumerator MoveToMainScene()
    {
        if (asyncOperation == null)
        {
            asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
    
    public void NewGameButtonClick()
    {
        StartCoroutine(nameof(NewGameStart));
    }
    public void CoroutineContinueButtonClick()
    {
        StartCoroutine(nameof(ContinueButtonClick));
    }
    public void CoroutineNewGameButtonClick()
    {
        if (Managers.data.GetIsFirstPlay())
        {
            NewGameButtonClick();
        }
        else if (!Managers.data.GetIsFirstPlay())
        {
            panel.SetActive(true);
            warning.SetActive(true);
        }
    }
    public void OptionButtonClick()
    {
        panel.SetActive(true);
        option.SetActive(true);
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(true);
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
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(false);
        option.SetActive(false);
        panel.SetActive(false);
    }
    public void CloseWarning()
    {
        panel.SetActive(false);
        warning.SetActive(false);
    }
}
