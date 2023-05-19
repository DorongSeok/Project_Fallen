using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("���� Ŭ���� ��������");
        StartCoroutine(nameof(Main_UISceneOpen));
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
