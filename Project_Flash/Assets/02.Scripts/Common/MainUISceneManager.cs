using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUISceneManager : MonoBehaviour
{
    public GameObject continueButton;
    void Start()
    {
        Managers.Sound.Clear();
        Managers.Sound.Play("BGM/LogoScene_BGM_2", Define.Sound.Bgm);

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
