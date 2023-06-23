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

    public GameData gData = new GameData(); // ��ũ��Ʈ ���� data ���� ���� ����
    public OptionData oData = new OptionData();

    public void LoadGameData() // ���� ���� ��ο� ����� �����͸� �ҷ���
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath) == true) // ����� �����Ͱ� �ִٸ�, �����͸� �ε��ϰ�, �ش� ������ data�� ������
        {
            string FromJsonData = File.ReadAllText(filePath);

            gData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }
    public void SaveGameData() // data���·� �����ϴ� ���� �ʿ� �����͸�, ���� ���Ϸ� ������
    {
        string ToJsonData = JsonUtility.ToJson(gData, true); // ������ ���� ���� ����
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData); // ������ �����Ѵٸ� ���� �����, �������� �ʴ´ٸ� ���� �����
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

    public void SetSavePos(Vector3 savePoint) // ������ ���� ���� ������� ������, savepoint�� ��ġ������ data�� ������
    {
        gData._savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
    }

    public Vector3 GetSavePos() // ����� ���� ���ϰ��� ������, savepoint�� ��ġ������ �ҷ���
    {
        string[] savePosArray = gData._savePos.Split(',');
        Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return pos;
    }

    // ���� get/set���� �� savePos ������ ���� ������ ����
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
    public void SetIsFirstPlay(bool isFirstPlay) // ������ ��ư ������ ��, false�� �����ϵ��� �ϰ�, ���� Ŭ���� ���� ��, true�� �����ϵ��� ��
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
    
    public void ResetSaveGameData() // ����� ��� ������ �ʱ�ȭ ��
    {
        gData = new GameData();
    }
    
}
