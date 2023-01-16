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

    public GameData data = new GameData(); // ��ũ��Ʈ ���� data ���� ���� ����

    IEnumerator Start() // ���� ���� ��, ������ �ε� ��, 2�� �� ���� �� ����
    {
        // �̱���
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
        DontDestroyOnLoad(gameObject);

        // �ε� ���ð�
        DataManager.instance.LoadGameData(); // ���� ���� ��, ������ �ε� ����
        yield return new WaitForSeconds(waitingTime);

        // Main�� �񵿱� �ε� ��� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

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
        DataManager.instance.data._savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
    }

    public Vector3 GetSavePos() // ����� ���� ���ϰ��� ������, savepoint�� ��ġ������ �ҷ���
    {
        string[] savePosArray = DataManager.instance.data._savePos.Split(',');
        Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return pos;
    }

    // ���� get/set���� �� savePos ������ ���� ������ ����
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
    public void ResetSavePoint() // ����� ��ġ�� �ʱ�ȭ ��
    {
        SetSavePos(Vector3.zero);
    }
}
