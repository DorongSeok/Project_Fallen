using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitingTime;
    IEnumerator Start() // 최초 실행 시, 데이터 로드 후, 2초 뒤 다음 씬 실행
    {
        // 싱글톤
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // 데이터 로드 관련 명령 입력

        // 여기까지

        // 로딩 대기시간
        yield return new WaitForSeconds(waitingTime);

        // Main씬 비동기 로드 사용 문법
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
