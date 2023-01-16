using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using DataInfo;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public float waitingTime = 1.0f;

    string GameDataFileName = "GameData.json";

    public GameData data = new GameData(); // 스크립트 내에 data 저장 공간 생성

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

    public void LoadGameData() // 로컬 파일 경로에 저장된 데이터를 불러옴
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath) == true) // 저장된 데이터가 있다면, 데이터를 로드하고, 해당 정보를 data에 적용함
        {
            string FromJsonData = File.ReadAllText(filePath);

            data = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }

    public void SaveGameData() // data형태로 존재하는 저장 필요 데이터를, 로컬 파일로 저장함
    {
        string ToJsonData = JsonUtility.ToJson(data, true); // 데이터 보기 좋게 정리
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData); // 파일이 존재한다면 덮어 씌우기, 존재하지 않는다면 새로 만들기
    }

    public void SetSavePos(Vector3 savePoint) // 데이터 로컬 파일 저장과는 별개로, savepoint의 위치정보를 data에 저장함
    {
        DataManager.instance.data._savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
    }

    public Vector3 GetSavePos() // 저장된 로컬 파일과는 별개로, savepoint의 위치정보를 불러옴
    {
        string[] savePosArray = DataManager.instance.data._savePos.Split(',');
        Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return pos;
    }

    // 이하 get/set문은 위 savePos 구문과 같은 구조를 가짐
    public void SetVelocity(Vector3 velocity)
    {
        DataManager.instance.data._velocity = velocity.x + "," + velocity.y + "," + velocity.z;
    }
    public Vector3 GetVelocity()
    {
        string[] velocityArray = DataManager.instance.data._velocity.Split(',');
        Vector3 velocity = new Vector3(float.Parse(velocityArray[0]), float.Parse(velocityArray[1]), float.Parse(velocityArray[2]));

        return velocity;
    }
    public void SetGravityScale(float gravityScale)
    {
        DataManager.instance.data._gravityScale = gravityScale;
    }
    public float GetGravityScale()
    {
        return DataManager.instance.data._gravityScale;
    }
    public void SetLinearDrag(float linearDrag)
    {
        DataManager.instance.data._linearDrag = linearDrag;
    }
    public float GetLinearDrag()
    {
        return DataManager.instance.data._linearDrag;
    }
    public void SetIsFallen(bool isFallen)
    {
        DataManager.instance.data._isFallen = isFallen;
    }
    public bool GetIsFallen()
    {
        return DataManager.instance.data._isFallen;
    }
    public void SetIsMove(bool isMove)
    {
        DataManager.instance.data._isMove = isMove;
    }
    public bool GetIsMove()
    {
        return DataManager.instance.data._isMove;
    }
    public void SetDuration(float duration)
    {
        DataManager.instance.data._duration = duration;
    }
    public float GetDuration()
    {
        return DataManager.instance.data._duration;
    }
    public void ResetSavePoint() // 저장된 위치를 초기화 함
    {
        SetSavePos(Vector3.zero);
    }
}
