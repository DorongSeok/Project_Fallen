using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public uint appid = 2365680;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        try
        {
            Steamworks.SteamClient.Init(appid, true);
        }
        catch
        {
            
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
