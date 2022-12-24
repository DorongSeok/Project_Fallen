using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public float waitingTime;

    private Vector3 savePoint; // ������ ������

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

    string GameDataFileName = "GameData.json";

    public GameData data = new GameData();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath)) // ����� �����Ͱ� �ִٸ�, �����͸� �ε��ϰ�, savePoint ���� ������ ���� ����
        {
            string FromJsonData = File.ReadAllText(filePath);

            data = JsonUtility.FromJson<GameData>(FromJsonData);

            string[] savePosArray = DataManager.instance.data.savePos.Split(',');
            Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));
            SetSavePoint(pos);
            Debug.Log("�ҷ����� �Ϸ�" + pos);
        }
    }

    public void SaveGameData() // data���·� �����ϴ� ���� �ʿ� �����͸�, ���� ���Ϸ� ������
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        File.WriteAllText(filePath, ToJsonData);

        Debug.Log("���� �Ϸ�" + ToJsonData);
    }
    private void OnApplicationQuit() // ���� ���� ��, ���� ����
    {
        DataManager.instance.SaveGameData();
    }

    public void SetSavePoint(Vector3 savePoint) // ������ ���� ���� ������� ������, savepoint�� ��ġ������ datamanager�� ������
    {
        this.savePoint = savePoint;
        DataManager.instance.data.savePos = savePoint.x + "," + savePoint.y + "," + savePoint.z;
        // DataManager.instance.SaveGameData();
    }

    public Vector3 GetSavePoint() // ����� ���� ���ϰ��� ������, savepoint�� ��ġ������ �ҷ���
    {
        // DataManager.instance.LoadGameData();

        // string[] savePosArray = DataManager.instance.data.savePos.Split(',');

        // Vector3 pos = new Vector3(float.Parse(savePosArray[0]), float.Parse(savePosArray[1]), float.Parse(savePosArray[2]));

        return savePoint;
        // savePoint;
    }
}
