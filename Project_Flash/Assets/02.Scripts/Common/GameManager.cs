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
            Debug.Log("�ν��Ͻ� ����!");
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("���� ����");
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
        Debug.Log("�������� ��ŸƮ!");
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
    }
    private void OnApplicationQuit() // ���� ���� ��, ���� ����
    {
        SaveData();
    }
    // ���� ���� �޴��� �̵� ��, ���� �÷��� ��Ȳ �����ϴ� ���� ������ ����
    public void ResumeButtonClick()
    {
        if (pauseMenuOpen == true)
        {
            PauseMenuClose();
        }
    }
    //private void PlayerActiveFalse()
    //{
    //    player.SetActive(false);
    //}
    private void SaveData()
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
    public void SaveAndExitButtonClick()
    {
        PauseMenuClose();
        SaveData();
        Managers.Instance.Clear();
        //PlayerActiveFalse();
        StartCoroutine(nameof(MoveToMainScene));
    }
    IEnumerator MoveToMainScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void PauseMenuOpen()
    {
        Debug.Log("�޴� ����");
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose()
    {
        Debug.Log("�޴� Ŭ����");
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
    }
}
