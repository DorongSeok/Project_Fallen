using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public float waitingTime;

    private Vector3 savePoint; // 저장할 데이터

    IEnumerator Start() // 최초 실행 시, 데이터 로드 후, 2초 뒤 다음 씬 실행
    {
        // 싱글톤
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // 로딩 대기시간
        DataManager.instance.LoadGameData(); // 게임 시작 시, 데이터 로드 진행
        yield return new WaitForSeconds(waitingTime);

        // Main씬 비동기 로드 사용 문법
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    string GameDataFileName = "GameData.json";

    public GameData data = new GameData();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath)) // 저장된 데이터가 있다면, 데이터를 로드하고, savePoint 값에 포지션 값을 대입
        {
            string FromJsonData = File.ReadAllText(filePath);

            data = JsonUtility.FromJson<GameData>(FromJsonData);

            string[] savePosArray = DataManager.instance.data.savePos.Split(',');
            Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));
            SetSavePoint(pos);
            Debug.Log("불러오기 완료" + pos);
        }
    }

    public void SaveGameData() // data형태로 존재하는 저장 필요 데이터를, 로컬 파일로 저장함
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("저장 완료" + ToJsonData);
    }
    private void OnApplicationQuit() // 게임 종료 시, 저장 진행
    {
        DataManager.instance.SaveGameData();
    }

    public void SetSavePoint(Vector3 savePoint) // 데이터 로컬 파일 저장과는 별개로, savepoint의 위치정보를 datamanager에 저장함
    {
        this.savePoint = savePoint;
        DataManager.instance.data.savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
        // DataManager.instance.SaveGameData();
    }

    public Vector3 GetSavePoint() // 저장된 로컬 파일과는 별개로, savepoint의 위치정보를 불러옴
    {
        // DataManager.instance.LoadGameData();

        // string[] savePosArray = DataManager.instance.data.savePos.Split(',');

        // Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return savePoint;
        // savePoint;
    }
}
