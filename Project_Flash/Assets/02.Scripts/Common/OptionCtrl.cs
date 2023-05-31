using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionCtrl : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    private FullScreenMode screenMode;
    private int resolutionNum;
    private bool firstOpen = true;

    private bool isOptionOpen = false;

    public Dropdown resolutionDropdown;
    public Toggle fullScreenBtn;
    public UIManager sceneManager;

    public AudioMixer masterMixer;
    public Slider BGMSlider;
    public Slider SFXSlider;

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

        BGMSlider.value = Managers.data.GetBGMSound();
        SFXSlider.value = Managers.data.GetSFXSound();

        firstOpen = false;
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
        if (firstOpen == false)
        {
            Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
        }
    }
    public void BGMControl()
    {
        if (firstOpen == false)
        {
            float sound = BGMSlider.value;

            if (sound <= -40.0f)
            {
                masterMixer.SetFloat("BGM", -80.0f);
            }
            else
            {
                masterMixer.SetFloat("BGM", sound);
            }
            Managers.data.SetBGMSound(sound);
        }
    }
    public void SFXControl()
    {
        if (firstOpen == false)
        {
            float sound = SFXSlider.value;

            if (sound <= -40f)
            {
                masterMixer.SetFloat("SFX", -80.0f);
            }
            else
            {
                masterMixer.SetFloat("SFX", sound);
            }
            Managers.data.SetSFXSound(sound);
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
