using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    private void Start()
    {
        ShowRecord();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.data.ResetSaveData();
            StartCoroutine(nameof(Main_UISceneOpen));
        }
    }

    IEnumerator Main_UISceneOpen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void ShowRecord()
    {
        Debug.Log("Clear Time : " + Managers.data.GetSecond());
        Debug.Log("Falling Count : " + Managers.data.GetFallenCount());
    }
}
