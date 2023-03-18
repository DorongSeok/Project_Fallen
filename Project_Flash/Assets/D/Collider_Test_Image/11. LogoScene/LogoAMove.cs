using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAMove : MonoBehaviour
{
    public float waitCount = 0.5f;
    // private int count = 0;
    // private float angle = 0;
    public float maxAngle = 3;
    public float minAngle = -3;
    public float moveSpeed = 1f;
    public int moveCount = 4;
    private float rotationZ = 0;
    private bool isMove = false;

    public GameObject logoCamera;
    public GameObject logoCharacter;

    void Start()
    {
        // count = 0;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetLogoAMove();
            logoCamera.GetComponent<LogoSceneMove>().MoveReset();
            logoCharacter.GetComponent<LogoSceneMove>().MoveReset();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StartCoroutine(nameof(ShakingA));
        }
        if (isMove == true)
        {
            rotationZ += Time.deltaTime * moveSpeed * 100.0f;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            if (rotationZ >= maxAngle || rotationZ <= minAngle)
            {
                moveSpeed *= -1;
            }
        }
    }
    IEnumerator ShakingA()
    {
        isMove = true;
        
        yield return new WaitForSeconds(waitCount);

        isMove = false;

        yield return new WaitForSeconds(waitCount);

        isMove = true;

        yield return new WaitForSeconds(waitCount);
        StartFalling();

        isMove = false;

        yield return new WaitForSeconds(waitCount * 0.5f);

    }

    private void StartFalling()
    {
        ResetLogoAMove();
        logoCamera.GetComponent<LogoSceneMove>().MoveStart();
        logoCharacter.GetComponent<LogoSceneMove>().MoveStart();
    }
    private void ResetLogoAMove()
    {
        rotationZ = 0;
        isMove = false;
        // count = 0;
        if (moveSpeed < 0)
        {
            moveSpeed *= -1;
        }
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
