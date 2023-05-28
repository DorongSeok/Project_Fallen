using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneMainContorller : MonoBehaviour
{
    public GameObject logoCamera = null;
    public GameObject logoCharacter = null;

    public float waitCount = 0.5f;
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            logoCamera.GetComponent<LogoSceneCameraController>().CameraReset();
            logoCharacter.GetComponent<LogoSceneCharacterController>().CharacterReset();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(nameof(LogoCutSceneStart));
        }
    }
    IEnumerator LogoCutSceneStart()
    {
        yield return new WaitForSeconds(1.0f);
        logoCamera.GetComponent<LogoSceneCameraController>().StartZoom();

        yield return new WaitForSeconds(2.5f);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StartShake();

        yield return new WaitForSeconds(waitCount);
        logoCharacter.GetComponent<LogoSceneCharacterController>().StopShake();

        logoCharacter.GetComponent<LogoSceneCharacterController>().StartFalling();
        logoCamera.GetComponent<LogoSceneCameraController>().StartFalling();

        yield return new WaitForSeconds(waitCount * 0.5f);
    }
}
