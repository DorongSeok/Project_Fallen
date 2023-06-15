using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCreditCtrl : MonoBehaviour
{
    public GameObject result;
    public GameObject endingCredit;
    public List<Image> endingCreditList = new List<Image>();
    public Image curtain;
    public Text text_PlayTime;
    public Text text_FallingCount;
    public EndingCameraMove endingCameraMove;

    public float fadeSpeed = 0.01f;
    private float fadeCount;
    private void Start()
    {
        EndingCreditStart();
    }
    private void EndingCreditStart()
    {
        endingCredit.SetActive(true);
        StartCoroutine(nameof(EndingCreditCoroutine));
    }
    IEnumerator EndingCreditCoroutine()
    {
        endingCameraMove.SetIsCamUpTrue();

        yield return new WaitForSeconds(2.0f);

        fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[0].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[0].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[1].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[1].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[2].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[2].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[3].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[3].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[4].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(1.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[4].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;
        yield return new WaitForSeconds(1.0f);
        while (fadeCount < 1.0f)
        {
            fadeCount += fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[5].color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(2.0f);
        while (fadeCount > 0.0f)
        {
            fadeCount -= fadeSpeed;
            yield return new WaitForSeconds(fadeSpeed);
            endingCreditList[5].color = new Color(255, 255, 255, fadeCount);
        }

        fadeCount = 0;

        yield return new WaitForSeconds(1.5f);

        endingCredit.SetActive(false);
        endingCameraMove.SetIsCamDownTrue();

        yield return new WaitForSeconds(3.0f);
        curtain.gameObject.SetActive(true);
        while (fadeCount < 1.0f)
        {
            fadeCount += (fadeSpeed * 0.5f);
            yield return new WaitForSeconds(fadeSpeed * 0.5f);
            curtain.color = new Color(0, 0, 0, fadeCount);
        }

        yield return new WaitForSeconds(0.5f);

        ShowResult();
    }
    private void ShowResult()
    {
        text_PlayTime.text = "playtime\ndddd";
        text_FallingCount.text = "fallingcount\ndddd";
        result.SetActive(true);
    }
}
