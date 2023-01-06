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


    public void StageStart() // �� ���� ���� �� ĳ���� ����
    {
        player = Instantiate(playerPrefab, DataManager.instance.GetSavePos(), Quaternion.identity);
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
    }
    private void OnApplicationQuit() // ���� ���� ��, ���� ����
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
    // ���� ���� �޴��� �̵� ��, ���� �÷��� ��Ȳ �����ϴ� ���� ������ ����
}
