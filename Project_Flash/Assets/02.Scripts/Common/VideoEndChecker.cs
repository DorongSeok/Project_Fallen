using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoEndChecker : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start() { videoPlayer.loopPointReached += CheckOver; }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("³¡");
        //videoPlayer.targetTexture.Release();
    }
}
