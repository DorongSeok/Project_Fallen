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

        if (Managers.Instance != null) // datamanger가 존재한다면 스테이지 시작 함수 실행(오류 방지)
        {
            StageStart();
        }

        // 존재하지 않는다면 종료하는 구문 필요 시 추가할 것
        //else if (DataManager.instance == null)
        //{
        //    Application.Quit();
        //}
    }
    private void OnKeyboard()
    {
        // 종료 키 입력 시 메뉴 오픈 여부에 따른 행동 반환
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

    public void StageStart() // 인 게임 시작 시 캐릭터 생성
    {
        player = Instantiate(playerPrefab, Managers.data.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
    }
    private void OnApplicationQuit() // 게임 종료 시, 저장 진행
    {
        SaveData();
    }
    public void ResumeButtonClick() // 일시정지 해제 시
    {
        if (pauseMenuOpen == true)
        {
            PauseMenuClose();
        }
    }
    private void SaveData() // 현 데이터 저장
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
    public void SaveAndExitButtonClick() // 저장 후 종료에 해당하는 버튼 클릭 시 대응하는 함수
    {
        PauseMenuClose();
        SaveData();
        Managers.Instance.Clear();
        StartCoroutine(nameof(MoveToMainScene));
    }
    IEnumerator MoveToMainScene() // 종료 시 메인 메뉴로 이동하는 함수
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    private void PauseMenuOpen() // 메뉴 오픈 시 게임 내 시간 정지
    {
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose() // 메뉴 오픈 해제 시 게임 내 시간 가동
    {
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
    }
}
