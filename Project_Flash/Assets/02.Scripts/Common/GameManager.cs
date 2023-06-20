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
        if (Managers.Instance != null) // datamanger가 존재한다면 스테이지 시작 함수 실행(오류 방지)
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
        // 추후 생성 코루틴 호출 중 오류가 생길 경우, 코루틴 종료 함수 실행할 것(혹은 while 조건으로)
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            second += 1;
        }
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

        makePerspective.SetPlayer(player);
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
                SaveGameManagerData();
                Managers.data.SaveGameData();
            }
        }
    }
    private void SaveGameManagerData()
    {
        Managers.data.SetSecond(second);
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
    private void PauseMenuOpen() // 메뉴 오픈 시 게임 내 시간 정지
    {
        playerCharacterControl.SetIsGameStop(true);
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
    }
    private void PauseMenuClose() // 메뉴 오픈 해제 시 게임 내 시간 가동
    {
        Time.timeScale = 1; 
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
        playerCharacterControl.SetIsGameStop(false);
    }
    public void ThemaChange(int Thema_Num)
    {
        nowThemaNum = Thema_Num;
        switch (nowThemaNum) // 여기 브금 입력해주면 됨
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
        // 이 부분에 브금 변환 입력해주면 됨
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
