using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks.Data;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public MakePerspective makePerspective;
    public UnityEngine.UI.Image curtain;
    public UnityEngine.UI.Text text_Height;
    public UnityEngine.UI.Text text_Time;
    public CameraMove mainCmr;

    private GameObject player;
    
    private PlayerCharacterControl playerCharacterControl;

    public GameObject pauseMenu;
    public TutorialCTRL tutorial;
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
        if (Managers.Instance != null) // datamanger가 존재한다면 스테이지 시작 함수 실행(오류 방지)
        {
            StageStart();
        }
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;



        second = Managers.data.GetSecond();

        StartCoroutine(nameof(TimerCoroutine));

        if (Managers.data.GetIsFirstPlay() == true)
        {
            PauseMenuOpen();
            TutorialButtonClick();
            Managers.data.SetIsFirstPlay(false);
        }
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyboard;
    }
    IEnumerator TimerCoroutine()
    {
        WaitForSeconds waitflag = new WaitForSeconds(1.0f);
        while(isGameEnd == false)
        {
            yield return waitflag;

            second += 1;
        }
    }
    private void OnKeyboard()
    {
        // 종료 키 입력 시 메뉴 오픈 여부에 따른 행동 반환
        if (isGameEnd == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (option.GetIsOptionOpen() == false && tutorial.GetIsOpen() == false)
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

    public void StageStart() // 인 게임 시작 시 캐릭터 생성
    {
        player = Instantiate(playerPrefab, Managers.data.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
        playerCharacterControl.SetText_Height(text_Height);

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
    public void TutorialButtonClick()
    {
        tutorial.gameObject.SetActive(true);
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
        Time.timeScale = 1;
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
        WaitForSeconds waitflag = new WaitForSeconds(fadeSpeed);

        curtain.gameObject.SetActive(true);
        playerCharacterControl.SetIsMoveStop(true);
        playerCharacterControl.SetIsGameEnd(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += (fadeSpeed);
            yield return waitflag;
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
    private void PauseMenuOpen() // 메뉴 오픈 시 게임 내 시간 정지
    {
        mainCmr.SetIsCameraCtrlFalse();
        playerCharacterControl.SetIsMoveStop(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pauseMenuOpen = true;
        pauseMenu.SetActive(true);
        text_Time.text = "Play Time\n" + getParseTime(second);
    }
    private void PauseMenuClose() // 메뉴 오픈 해제 시 게임 내 시간 가동
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseMenuOpen = false;
        pauseMenu.SetActive(false);
        playerCharacterControl.SetIsMoveStop(false);
    }
    public void ThemaChange(int Thema_Num)
    {
        nowThemaNum = Thema_Num;
        switch (nowThemaNum) // 여기 브금 입력해주면 됨
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
    private string getParseTime(float time)
    {
        string t = TimeSpan.FromSeconds(time).ToString("hh\\:mm\\:ss");
        string[] tokens = t.Split(':');
        return tokens[0] + "h " + tokens[1] + "m " + tokens[2] + "s";
    }
}
