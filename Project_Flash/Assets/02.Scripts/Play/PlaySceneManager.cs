using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour
{
    public Transform startPos;

    public GameObject magnetic;
    private void Awake()
    {
        GameManager.instance.SetSavePoint(startPos);
        GameManager.instance.SetMagnetic(magnetic);
        GameManager.instance.StageStart();
    }
}
