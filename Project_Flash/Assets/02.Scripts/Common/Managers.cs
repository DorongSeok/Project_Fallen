using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Managers : MonoBehaviour
{
    public float waitingTime = 1.0f;
    public AudioMixer masterMixer;

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
        OptionSetting();
        ViewTeamLogo();
        yield return new WaitForSeconds(waitingTime); // ����� ��ü�� ��

        // Main�� �񵿱� �ε� ��� ����
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
        _data.SaveOptionData();
        Time.timeScale = 0.0f;
    }
    static void Init() // �̱���
    {
        if (s_instance == null && Time.timeScale != 0.0f)
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
            s_instance._data.LoadOptionData();
        }
    }
    private void ViewTeamLogo()
    {
        //���ΰ� ���� �Է�
    }
    public void Clear() // �ʱ�ȭ
    {
        Input.Clear();
        Sound.Clear();
    }
    private void OptionSetting()
    {
        if (Managers.data.GetIsStarted() == false)
        {
            Screen.SetResolution(1920, 1080, true);
            Managers.data.SetIsStarted(true);
        }
        if (Managers.data.GetBGMSound() <= -40.0f)
        {
            masterMixer.SetFloat("BGM", -80.0f);
        }
        else
        {
            masterMixer.SetFloat("BGM", Managers.data.GetBGMSound());
        }
        if (Managers.data.GetSFXSound() <= -40f)
        {
            masterMixer.SetFloat("SFX", -80.0f);
        }
        else
        {
            masterMixer.SetFloat("SFX", Managers.data.GetSFXSound());
        }
        
    }
}
