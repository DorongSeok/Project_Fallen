using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    private GameObject player;
    
    // public GameObject player;
    private PlayerCharacterControl playerCharacterControl;

    public GameObject pauseMenu;
    public OptionCtrl option;
    private bool pauseMenuOpen = false;

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        if (Managers.Instance != null) // datamanger�� �����Ѵٸ� �������� ���� �Լ� ����(���� ����)
        {
            StageStart();
        }

        // �������� �ʴ´ٸ� �����ϴ� ���� �ʿ� �� �߰��� ��
        //else if (DataManager.instance == null)
        //{
        //    Application.Quit();
        //}
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
                Managers.data.SaveGameData();
            }
        }
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
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void PauseMenuOpen() // �޴� ���� �� ���� �� �ð� ����
    {
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose() // �޴� ���� ���� �� ���� �� �ð� ����
    {
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
    }
}
