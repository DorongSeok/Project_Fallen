using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_CutSceneManager : MonoBehaviour
{
    IEnumerator CheckSceneCoroutine;

    public GameObject logoCamera = null;
    public GameObject logoCharacter = null;
    public Canvas canvas;

    public float waitCount = 1.1f;

    private bool isCutSceneEnd = false;

    private bool isBgmChanging = false;

    void Start()
    {
        CheckSceneCoroutine = LogoCutSceneStart();
        StartCoroutine(CheckSceneCoroutine);

        Managers.Sound.Play("BGM/LogoScene_BGM_1", Define.Sound.Bgm);
        isBgmChanging = false;
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (isCutSceneEnd == false)
            {
                if (CheckSceneCoroutine != null)
                {
                    StopCoroutine(CheckSceneCoroutine);
                }
                StartCoroutine(nameof(Main_UISceneOpen));
            }
        }
    }
    IEnumerator LogoCutSceneStart()
    {
        yield return new WaitForSeconds(2.0f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();
        Managers.Sound.Play("Effect/LogoScene_ShakeEffect");

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        yield return new WaitForSeconds(waitCount * 1.5f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();
        Managers.Sound.Play("Effect/LogoScene_ShakeEffect");

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        yield return new WaitForSeconds(1.0f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartFalling();
        logoCamera.GetComponent<LogoSceneCameraController>().StartFalling();
        Managers.Sound.Play("Effect/LogoScene_FallingEffect");

        yield return new WaitForSeconds(0.1f);
        logoCamera.GetComponent<LogoSceneCameraController>().StartZoom();

        yield return new WaitForSeconds(2.5f);
        isCutSceneEnd = true;

        yield return new WaitForSeconds(4.3f);
        canvas.gameObject.SetActive(true);
    }
    IEnumerator Main_UISceneOpen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ChangeBGM()
    {
        if (isBgmChanging == false)
        {
            Managers.Sound.Play("BGM/LogoScene_BGM_2", Define.Sound.Bgm);
            isBgmChanging = true;
        }
    }
}
