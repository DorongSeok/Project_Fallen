using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSceneManager : MonoBehaviour
{
    private UIManager sceneManager;
    void Start()
    {
        sceneManager = this.GetComponent<UIManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.CoroutineMoveToMainScene();
        }
    }
}
