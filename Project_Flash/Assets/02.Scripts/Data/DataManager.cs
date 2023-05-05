using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using DataInfo;

public class DataManager
{
    string GameDataFileName = "GameData.json";

    public GameData data = new GameData(); // ��ũ��Ʈ ���� data ���� ���� ����

    public void LoadGameData() // ���� ���� ��ο� ����� �����͸� �ҷ���
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath) == true) // ����� �����Ͱ� �ִٸ�, �����͸� �ε��ϰ�, �ش� ������ data�� ������
        {
            string FromJsonData = File.ReadAllText(filePath);

            data = JsonUtility.FromJson<GameData>(FromJsonData);
        }
    }

    public void SaveGameData() // data���·� �����ϴ� ���� �ʿ� �����͸�, ���� ���Ϸ� ������
    {
        string ToJsonData = JsonUtility.ToJson(data, true); // ������ ���� ���� ����
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData); // ������ �����Ѵٸ� ���� �����, �������� �ʴ´ٸ� ���� �����
    }

    public void SetSavePos(Vector3 savePoint) // ������ ���� ���� ������� ������, savepoint�� ��ġ������ data�� ������
    {
        data._savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
    }

    public Vector3 GetSavePos() // ����� ���� ���ϰ��� ������, savepoint�� ��ġ������ �ҷ���
    {
        string[] savePosArray = data._savePos.Split(',');
        Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return pos;
    }

    // ���� get/set���� �� savePos ������ ���� ������ ����
    public void SetVelocity(Vector3 velocity)
    {
        data._velocity = velocity.x + "," + velocity.y + "," + velocity.z;
    }
    public Vector3 GetVelocity()
    {
        string[] velocityArray = data._velocity.Split(',');
        Vector3 velocity = new Vector3(float.Parse(velocityArray[0]), float.Parse(velocityArray[1]), float.Parse(velocityArray[2]));

        return velocity;
    }
    public void SetGravityScale(float gravityScale)
    {
        data._gravityScale = gravityScale;
    }
    public float GetGravityScale()
    {
        return data._gravityScale;
    }
    public void SetLinearDrag(float linearDrag)
    {
        data._linearDrag = linearDrag;
    }
    public float GetLinearDrag()
    {
        return data._linearDrag;
    }
    public void SetIsFallen(bool isFallen)
    {
        data._isFallen = isFallen;
    }
    public bool GetIsFallen()
    {
        return data._isFallen;
    }
    public void SetIsMove(bool isMove)
    {
        data._isMove = isMove;
    }
    public bool GetIsMove()
    {
        return data._isMove;
    }
    public void SetDuration(float duration)
    {
        data._duration = duration;
    }
    public float GetDuration()
    {
        return data._duration;
    }
    public void SetIsignoreLayerCollision(bool isignoreLayerCollision)
    {
        data._isignoreLayerCollision = isignoreLayerCollision;
    }
    public bool GetIsignoreLayerCollision()
    {
        return data._isignoreLayerCollision;
    }
    public void SetIsFirstPlay(bool isFirstPlay) // ������ ��ư ������ ��, false�� �����ϵ��� �ϰ�, ���� Ŭ���� ���� ��, true�� �����ϵ��� ��
    {
        data._isFirstPlay = isFirstPlay;
    }
    public bool GetIsFirstPlay()
    {
        return data._isFirstPlay;
    }
    public void SetLevel(int level)
    {
        data._level = level;
    }
    public int GetLevel()
    {
        return data._level;
    }
    public void ResetSaveData() // ����� ��� ������ �ʱ�ȭ ��
    {
        data = new GameData();
    }
}
