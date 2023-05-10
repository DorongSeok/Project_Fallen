using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditSceneManager : MonoBehaviour
{
    private UIManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = this.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.CoroutineMoveToMainScene();
        }
    }
}
