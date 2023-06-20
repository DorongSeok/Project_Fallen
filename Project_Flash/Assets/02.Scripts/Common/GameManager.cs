using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks.Data;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public MakePerspective makePerspective;

    private GameObject player;
    
    private PlayerCharacterControl playerCharacterControl;

    public GameObject pauseMenu;
    public OptionCtrl option;
    private bool pauseMenuOpen = false;

    private float second = 0.0f;

    private int nowThemaNum = 0;
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void GameClear()
    {
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
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;

            case 5:
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
