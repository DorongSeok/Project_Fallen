using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAMove : MonoBehaviour
{
    private int count = 0;
    private float angle = 0;
    public float maxAngle = 4;
    public float minAngle = -4;
    public float moveSpeed = 0.1f;
    public int moveCount = 4;
    private float rotationZ = 0;
    private bool isMove = false;

    public GameObject logoCamera;
    public GameObject logoCharacter;

    void Start()
    {
        count = 0;
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
            isMove = true;
        }

        if(isMove == true)
        {
            rotationZ += moveSpeed;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            if(rotationZ >=maxAngle || rotationZ<=minAngle)
            {
                moveSpeed *= -1;
                count++;
            }

            if (count >= moveCount && moveSpeed < 0 && rotationZ < 0)
            {
                StartFalling();
            }
            else if (count >= moveCount && moveSpeed > 0 && rotationZ > 0)
            {
                StartFalling();
            }
        }
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
        count = 0;
        if (moveSpeed < 0)
        {
            moveSpeed *= -1;
        }
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
