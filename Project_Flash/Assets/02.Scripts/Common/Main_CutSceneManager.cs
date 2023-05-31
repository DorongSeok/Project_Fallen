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
    void Start()
    {
        CheckSceneCoroutine = LogoCutSceneStart();
        StartCoroutine(CheckSceneCoroutine);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
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
        yield return new WaitForSeconds(0.7f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();

        yield return new WaitForSeconds(waitCount * 0.5f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        yield return new WaitForSeconds(1.0f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartFalling();
        logoCamera.GetComponent<LogoSceneCameraController>().StartFalling();

        yield return new WaitForSeconds(0.1f);
        logoCamera.GetComponent<LogoSceneCameraController>().StartZoom();

        yield return new WaitForSeconds(2.5f);
        isCutSceneEnd = true;

        yield return new WaitForSeconds(1.5f);
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
}
