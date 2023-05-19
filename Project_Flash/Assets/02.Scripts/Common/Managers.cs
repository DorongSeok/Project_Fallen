using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public float waitingTime = 1.0f;

    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();

    public static DataManager data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }

    IEnumerator Start()
    {
        Init();
        yield return new WaitForSeconds(waitingTime); // 연출로 대체할 것

        // Main씬 비동기 로드 사용 문법
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void Update()
    {
        _input.OnUpdate();
    }
    private void OnApplicationQuit()
    {
        _data.SaveGameData();
    }
    static void Init() // 싱글톤
    {
        if(s_instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();
            s_instance._sound.Init();
            s_instance._data.LoadGameData();
        }
    }

    public void Clear() // 초기화
    {
        Input.Clear();
        Sound.Clear();
    }
}
