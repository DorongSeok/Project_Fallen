using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    private FullScreenMode screenMode;
    private int resolutionNum;


    private bool isOptionOpen = false;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenBtn;
    public UIManager sceneManager;

    private void Start()
    {
        InitUI();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManager.OptionExitButtonClick();
        }
    }
    private void InitUI()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height;// +" " + item.refreshRate + "hz" 주사율도 필요하면 앞에 구문 추가할 것
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullScreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        ChangeResolution();
    }
    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
        ChangeResolution();
    }
    public void ChangeResolution()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
        Debug.Log("해상도 변경 완료");
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
