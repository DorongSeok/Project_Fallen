using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    // 게임 실행 시, 해당 게임을 실행중에 있음을 표시하게 해주는 기능(예제)
    // 추후 기능 추가 예정
    public uint appid;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        try
        {
            Steamworks.SteamClient.Init(appid, true);
            Debug.Log("Steam is up and running");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        try
        {
            Steamworks.SteamClient.Shutdown();
        }
        catch
        {

        }
    }
}
