using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

public class SteamManager : MonoBehaviour
{
    const uint appid = 2365680;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        try
        {
            SteamClient.Init(appid, true);
        }
        catch
        {
            
        }
    }
    
    private void OnApplicationQuit()
    {
        try
        {
            SteamClient.Shutdown();
        }
        catch
        {

        }
    }
}
