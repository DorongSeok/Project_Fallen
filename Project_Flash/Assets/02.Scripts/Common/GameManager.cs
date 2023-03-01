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
        if (DataManager.instance != null) // datamanger가 존재한다면 스테이지 시작 함수 실행(오류 방지)
        {
            Debug.Log("인스턴스 존재!");
            StageStart();
        }

        // 존재하지 않는다면 종료하는 구문 필요 시 추가할 것
        //else if (DataManager.instance == null)
        //{
        //    Application.Quit();
        //}
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("종료 누름");
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

    public void StageStart() // 인 게임 시작 시 캐릭터 생성
    {
        Debug.Log("스테이지 스타트!");
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
    }
    private void OnApplicationQuit() // 게임 종료 시, 저장 진행
    {
        SaveData();
    }
    // 추후 메인 메뉴로 이동 시, 게임 플레이 상황 저장하는 구문 생성할 예정
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
        Debug.Log("메뉴 오픈");
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose()
    {
        Debug.Log("메뉴 클로즈");
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
    }
}
