using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{
    public UIManager sceneManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.OptionExitButtonClick();
        }
    }
}
