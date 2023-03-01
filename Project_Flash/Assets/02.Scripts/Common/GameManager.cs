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
    private bool pauseMenuOpen = false;

    void Start() 
    {
        if (DataManager.instance != null) // datamanger�� �����Ѵٸ� �������� ���� �Լ� ����(���� ����)
        {
            StageStart();
        }

        // �������� �ʴ´ٸ� �����ϴ� ���� �ʿ� �� �߰��� ��
        //else if (DataManager.instance == null)
        //{
        //    Application.Quit();
        //}
    }
    private void Update()
    {
        // ���� Ű �Է� �� �޴� ���� ���ο� ���� �ൿ ��ȯ
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void StageStart() // �� ���� ���� �� ĳ���� ����
    {
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePos(), Quaternion.identity);
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
        if (DataManager.instance != null)
        {
            if (playerCharacterControl != null)
            {
                playerCharacterControl.SavePlayerData();
                DataManager.instance.SaveGameData();
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
