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

    private AsyncOperation asyncOperation;

    IEnumerator ContinueButtonClick()
    {
        if (asyncOperation == null)
        {
            //AsyncOperation asyncOperation;
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
    IEnumerator NewGameButtonClick()
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
        //eventSystem.sendNavigationEvents = false;
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
        //eventSystem.sendNavigationEvents = true; // 추후 네비게이션 기능 사용할 때 활성화할 것
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(false);
        option.SetActive(false);
    }
}
