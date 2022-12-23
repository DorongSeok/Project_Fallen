using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;

    private Transform savePoint;
    private GameObject player;
    private GameObject magnetic;
    private Magnetic_FieldMove magnetic_FieldMove;

    public float reviveTime;
    public float waitingTime;
    IEnumerator Start() // ���� ���� ��, ������ �ε� ��, 2�� �� ���� �� ����
    {
        // �̱���
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // �ε� ���ð�
        yield return new WaitForSeconds(waitingTime);

        // Main�� �񵿱� �ε� ��� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void StageStart()
    {
        player = Instantiate(playerPrefab, savePoint.position, Quaternion.identity);
        magnetic_FieldMove.ReSetPosition(player.transform);
    }

    IEnumerator StageRestart()
    {
        yield return new WaitForSeconds(reviveTime);
        player = Instantiate(playerPrefab, savePoint.position, Quaternion.identity);
        magnetic_FieldMove.ReSetPosition(player.transform);
    }
    public void CoroutineStageRestart()
    {
        StartCoroutine(nameof(StageRestart));
    }
    public void SetSavePoint(Transform savePoint)
    {
        this.savePoint = savePoint;
    }
    public Transform GetSavePoint()
    {
        return savePoint;
    }
    public void SetMagnetic(GameObject magnetic)
    {
        this.magnetic = magnetic;
        magnetic_FieldMove = this.magnetic.GetComponent<Magnetic_FieldMove>();
    }
}
