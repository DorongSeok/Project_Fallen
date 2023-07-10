using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public GameObject continueButton;
    void Start()
    {
        Managers.Sound.Clear();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (Managers.data.GetIsFirstPlay() == true)
        {
            continueButton.GetComponent<Button>().interactable = false;
        }
        else if (Managers.data.GetIsFirstPlay() == false)
        {
            continueButton.GetComponent<Button>().interactable = true;
        }
    }
}
