using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCTRL : MonoBehaviour
{
    public List<Image> tutorialImages = new List<Image>();

    private int nowImageNum;
    private bool isOpen;

    private void OnEnable()
    {
        nowImageNum = 0;
        isOpen = true;
        tutorialImages[nowImageNum].gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TutorialImageClose();
        }
    }
    public void TutorialImageClose()
    {
        foreach (var item in tutorialImages)
        {
            item.gameObject.SetActive(false);
        }
        isOpen = false;
        this.gameObject.SetActive(false);
    }
    public void LeftButtonClick()
    {
        if (nowImageNum > 0)
        {
            nowImageNum -= 1;
            tutorialImages[nowImageNum + 1].gameObject.SetActive(false);
            tutorialImages[nowImageNum].gameObject.SetActive(true);
        }
    }
    public void RightButtonClick()
    {
        if (nowImageNum < tutorialImages.Count - 1)
        {
            nowImageNum += 1;
            tutorialImages[nowImageNum - 1].gameObject.SetActive(false);
            tutorialImages[nowImageNum].gameObject.SetActive(true);
        }
    }
    public bool GetIsOpen()
    {
        return isOpen;
    }
}
