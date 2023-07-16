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
    public GameObject credit;

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
            Managers.data.SetLevel(1);
            asyncOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
    IEnumerator RankingButtonClick()
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
        panel.SetActive(true); // 씬 로드 중 다른 버튼 터치 불가능하도록 막음
        StartCoroutine(nameof(ContinueButtonClick));
    }
    public void CoroutineNewGameButtonClick()
    {
        panel.SetActive(true);
        if (Managers.data.GetIsFirstPlay())
        {
            NewGameButtonClick();
        }
        else if (!Managers.data.GetIsFirstPlay())
        {
            warning.SetActive(true); // 데이터 덮어씌워진다는 경고 문구 출력
        }
    }
    public void OptionButtonClick()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
        option.SetActive(true);
        option.GetComponent<OptionCtrl>().SetIsOptionOpen(true);
    }
    public void CoroutineRankingButtonClick()
    {
        StartCoroutine(nameof(RankingButtonClick));
    }
    public void CreditButtonClick()
    {
        credit.SetActive(true);
    }
    public void CloseCredit()
    {
        credit.SetActive(false);
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
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
    public void CloseWarning()
    {
        panel.SetActive(false);
        warning.SetActive(false);
    }
}
