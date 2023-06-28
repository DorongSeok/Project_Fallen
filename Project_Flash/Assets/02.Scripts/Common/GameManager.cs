using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks.Data;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public MakePerspective makePerspective;
    public UnityEngine.UI.Image curtain; 

    private GameObject player;
    
    private PlayerCharacterControl playerCharacterControl;

    public GameObject pauseMenu;
    public OptionCtrl option;
    private bool pauseMenuOpen = false;
    private bool isGameEnd = false;

    private float second = 0.0f;

    private int nowThemaNum = 0;

    private float fadeCount = 0.0f;
    private float fadeSpeed = 0.01f;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
    void Start()
    {
        if (Managers.Instance != null) // datamanger�� �����Ѵٸ� �������� ���� �Լ� ����(���� ����)
        {
            StageStart();
        }
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;



        second = Managers.data.GetSecond();

        StartCoroutine(nameof(TimerCoroutine));
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyboard;
    }
    IEnumerator TimerCoroutine()
    {
        // ���� ���� �ڷ�ƾ ȣ�� �� ������ ���� ���, �ڷ�ƾ ���� �Լ� ������ ��(Ȥ�� while ��������)
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            second += 1;
        }
    }
    private void OnKeyboard()
    {
        // ���� Ű �Է� �� �޴� ���� ���ο� ���� �ൿ ��ȯ
        if (isGameEnd == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (option.GetIsOptionOpen() == false)
                {
                    if (pauseMenuOpen == false)
                    {
                        PauseMenuOpen();
                    }
                    else if (pauseMenuOpen == true)
                    {
                        PauseMenuClose();
                    }
                }
            }
        }
    }

    public void StageStart() // �� ���� ���� �� ĳ���� ����
    {
        player = Instantiate(playerPrefab, Managers.data.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();

        makePerspective.SetPlayer(player);
    }
    private void OnApplicationQuit() // ���� ���� ��, ���� ����
    {
        SaveData();
    }
    public void ResumeButtonClick() // �Ͻ����� ���� ��
    {
        if (pauseMenuOpen == true)
        {
            PauseMenuClose();
        }
    }
    private void SaveData() // �� ������ ����
    {
        if (Managers.data != null)
        {
            if (playerCharacterControl != null)
            {
                playerCharacterControl.SavePlayerData();
                SaveGameManagerData();
                Managers.data.SaveGameData();
            }
        }
    }
    private void SaveGameManagerData()
    {
        Managers.data.SetSecond(second);
    }
    public void SaveAndExitButtonClick() // ���� �� ���ῡ �ش��ϴ� ��ư Ŭ�� �� �����ϴ� �Լ�
    {
        PauseMenuClose();
        SaveData();
        Managers.Instance.Clear();
        StartCoroutine(nameof(MoveToMainScene));
    }
    IEnumerator MoveToMainScene() // ���� �� ���� �޴��� �̵��ϴ� �Լ�
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    IEnumerator EndingSceneOpen()
    {
        curtain.gameObject.SetActive(true);
        playerCharacterControl.SetIsGameStop(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += (fadeSpeed);
            yield return new WaitForSeconds(fadeSpeed);
            curtain.color = new UnityEngine.Color(0, 0, 0, fadeCount);
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void GameClear()
    {
        isGameEnd = true;
        try
        {
            var ach = new Achievement("CLEAR_ALL_THEMA");
            ach.Trigger();
        }
        catch
        {
            
        }
        SaveData();
        StartCoroutine(nameof(EndingSceneOpen));
    }
    private void PauseMenuOpen() // �޴� ���� �� ���� �� �ð� ����
    {
        playerCharacterControl.SetIsGameStop(true);
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose() // �޴� ���� ���� �� ���� �� �ð� ����
    {
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
        playerCharacterControl.SetIsGameStop(false);
    }
    public void ThemaChange(int Thema_Num)
    {
        nowThemaNum = Thema_Num;
        switch (nowThemaNum) // ���� ��� �Է����ָ� ��
        {
            case 1:
                Managers.Sound.Play("BGM/t1_BGM", Define.Sound.Bgm);
                break;

            case 2:
                Managers.Sound.Play("BGM/t2_BGM", Define.Sound.Bgm);
                try
                {
                    var ach = new Achievement("CLEAR_1_THEMA");
                    ach.Trigger();
                }
                catch
                {

                }
                break;

            case 3:
                Managers.Sound.Play("BGM/t3_BGM", Define.Sound.Bgm);
                try
                {
                    var ach = new Achievement("CLEAR_2_THEMA");
                    ach.Trigger();
                }
                catch
                {

                }
                break;

            case 4:
                Managers.Sound.Play("BGM/t4_BGM", Define.Sound.Bgm);
                try
                {
                    var ach = new Achievement("CLEAR_3_THEMA");
                    ach.Trigger();
                }
                catch
                {

                }
                break;

            case 5:
                Managers.Sound.Play("BGM/t5_BGM", Define.Sound.Bgm);
                try
                {
                    var ach = new Achievement("CLEAR_4_THEMA");
                    ach.Trigger();
                }
                catch
                {

                }
                break;

            default:
                break;
        }
        // �� �κп� ��� ��ȯ �Է����ָ� ��
    }

    public void SetSecond(float second)
    {
        this.second = second;
    }
    public float GetSecond()
    {
        return second;
    }
    
}
