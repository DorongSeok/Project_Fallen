using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            StartCoroutine(nameof(EndingSceneOpen));
        }
    }
    IEnumerator EndingSceneOpen()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(6, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
