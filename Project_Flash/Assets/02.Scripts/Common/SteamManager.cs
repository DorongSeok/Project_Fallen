using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    // ���� ���� ��, �ش� ������ �����߿� ������ ǥ���ϰ� ���ִ� ���(����)
    // ���� ��� �߰� ����
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
