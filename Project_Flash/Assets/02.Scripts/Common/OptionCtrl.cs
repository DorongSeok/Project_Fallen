using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{
    private bool isOptionOpen = false;

    public UIManager sceneManager;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.OptionExitButtonClick();
        }
    }
    public void SetIsOptionOpen(bool isOptionOpen)
    {
        this.isOptionOpen = isOptionOpen;
    }
    public bool GetIsOptionOpen()
    {
        return isOptionOpen;
    }
}
