using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
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
