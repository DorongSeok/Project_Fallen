using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    
    private GameObject player;
    private PlayerCharacterControl playerCharacterControl;

    void Start() 
    {
        if (DataManager.instance != null) // datamanger가 존재한다면 스테이지 시작 함수 실행(오류 방지)
        {
            StageStart();
        }

        // 존재하지 않는다면 종료하는 구문 필요 시 추가할 것
        //else if (DataManager.instance == null)
        //{
        //    Application.Quit();
        //}
    }


    public void StageStart() // 인 게임 시작 시 캐릭터 생성
    {
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
    }
    private void OnApplicationQuit() // 게임 종료 시, 저장 진행
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
    // 추후 메인 메뉴로 이동 시, 게임 플레이 상황 저장하는 구문 생성할 예정
}
