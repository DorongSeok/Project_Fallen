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
    IEnumerator Start() // 최초 실행 시, 데이터 로드 후, 2초 뒤 다음 씬 실행
    {
        // 싱글톤
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // 로딩 대기시간
        yield return new WaitForSeconds(waitingTime);

        // Main씬 비동기 로드 사용 문법
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
