using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using DataInfo;

public class DataManager
{
    string GameDataFileName = "GameData.json";
    string OptionDataFileName = "OptionData.json";

    public GameData gData = new GameData(); // 스크립트 내에 data 저장 공간 생성
    public OptionData oData = new OptionData();

    public void LoadGameData() // 로컬 파일 경로에 저장된 데이터를 불러옴
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath) == true) // 저장된 데이터가 있다면, 데이터를 로드하고, 해당 정보를 data에 적용함
        {
            string FromJsonData = File.ReadAllText(filePath);

            gData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }
    public void SaveGameData() // data형태로 존재하는 저장 필요 데이터를, 로컬 파일로 저장함
    {
        string ToJsonData = JsonUtility.ToJson(gData, true); // 데이터 보기 좋게 정리
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData); // 파일이 존재한다면 덮어 씌우기, 존재하지 않는다면 새로 만들기
    }
    public void LoadOptionData()
    {
        string filePath = Application.persistentDataPath + "/" + OptionDataFileName;
        if (File.Exists(filePath) == true)
        {
            string FromJsonData = File.ReadAllText(filePath);

            oData = JsonUtility.FromJson<OptionData>(FromJsonData);
        }
    }
    public void SaveOptionData()
    {
        string ToJsonData = JsonUtility.ToJson(oData, true);
        string filePath = Application.persistentDataPath + "/" + OptionDataFileName;

        File.WriteAllText(filePath, ToJsonData);
    }

    public void SetSavePos(Vector3 savePoint) // 데이터 로컬 파일 저장과는 별개로, savepoint의 위치정보를 data에 저장함
    {
        gData._savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
    }

    public Vector3 GetSavePos() // 저장된 로컬 파일과는 별개로, savepoint의 위치정보를 불러옴
    {
        string[] savePosArray = gData._savePos.Split(',');
        Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return pos;
    }

    // 이하 get/set문은 위 savePos 구문과 같은 구조를 가짐
    public void SetVelocity(Vector3 velocity)
    {
        gData._velocity = velocity.x + "," + velocity.y + "," + velocity.z;
    }
    public Vector3 GetVelocity()
    {
        string[] velocityArray = gData._velocity.Split(',');
        Vector3 velocity = new Vector3(float.Parse(velocityArray[0]), float.Parse(velocityArray[1]), float.Parse(velocityArray[2]));

        return velocity;
    }
    public void SetGravityScale(float gravityScale)
    {
        gData._gravityScale = gravityScale;
    }
    public float GetGravityScale()
    {
        return gData._gravityScale;
    }
    public void SetLinearDrag(float linearDrag)
    {
        gData._linearDrag = linearDrag;
    }
    public float GetLinearDrag()
    {
        return gData._linearDrag;
    }
    public void SetIsFallen(bool isFallen)
    {
        gData._isFallen = isFallen;
    }
    public bool GetIsFallen()
    {
        return gData._isFallen;
    }
    public void SetIsMove(bool isMove)
    {
        gData._isMove = isMove;
    }
    public bool GetIsMove()
    {
        return gData._isMove;
    }
    public void SetDuration(float duration)
    {
        gData._duration = duration;
    }
    public float GetDuration()
    {
        return gData._duration;
    }
    public void SetIsignoreLayerCollision(bool isignoreLayerCollision)
    {
        gData._isignoreLayerCollision = isignoreLayerCollision;
    }
    public bool GetIsignoreLayerCollision()
    {
        return gData._isignoreLayerCollision;
    }
    public void SetIsFirstPlay(bool isFirstPlay) // 뉴게임 버튼 눌렀을 때, false를 저장하도록 하고, 게임 클리어 했을 때, true를 저장하도록 함
    {
        gData._isFirstPlay = isFirstPlay;
    }
    public bool GetIsFirstPlay()
    {
        return gData._isFirstPlay;
    }
    public void SetLevel(int level)
    {
        gData._level = level;
    }
    public int GetLevel()
    {
        return gData._level;
    }
    public void SetFallenCount(int fallenCount)
    {
        gData._fallenCount = fallenCount;
    }
    public int GetFallenCount()
    {
        return gData._fallenCount;
    }
    public void SetSecond(float second)
    {
        gData._second = second;
    }
    public float GetSecond()
    {
        return gData._second;
    }
    public void SetIsBugCheckerActive(bool isBugCheckerActive)
    {
        gData._isBugCheckerActive = isBugCheckerActive;
    }
    public bool GetIsBugCheckerActive()
    {
        return gData._isBugCheckerActive;
    }
    public void SetBGMSound(float bgmSound)
    {
        oData._bgmSound = bgmSound;
    }
    public float GetBGMSound()
    {
        return oData._bgmSound;
    }
    public void SetSFXSound(float sfxSound)
    {
        oData._sfxSound = sfxSound;
    }
    public float GetSFXSound()
    {
        return oData._sfxSound;
    }
    public void SetIsStarted(bool isStarted)
    {
        oData._isStarted = isStarted;
    }
    public bool GetIsStarted()
    {
        return oData._isStarted;
    }
    
    public void ResetSaveGameData() // 저장된 모든 정보를 초기화 함
    {
        gData = new GameData();
    }
    
}
