using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitingTime;
    IEnumerator Start() // ���� ���� ��, ������ �ε� ��, 2�� �� ���� �� ����
    {
        // �̱���
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // ������ �ε� ���� ��� �Է�

        // �������

        // �ε� ���ð�
        yield return new WaitForSeconds(waitingTime);

        // Main�� �񵿱� �ε� ��� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
