using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCameraMove : MonoBehaviour
{
    //���� ���� �������� �ð��� �����ϸ� ��
    public float spd;
    public float damping;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 speed;

    private bool isCamUp = false;
    private bool isCamDown = false;
    private void Start()
    {
        startPos = transform.localPosition;
        endPos = transform.localPosition + (Vector3.down * damping);
        speed = Vector3.zero;
    }
    private void Update()
    {
        if (isCamUp)
        {
            MoveCamUp();
        }
        if (isCamDown)
        {
            MoveCamDown();
        }
    }
    public void MoveCamUp()
    {
       transform.localPosition = Vector3.SmoothDamp(transform.localPosition, endPos, ref speed, spd);
        if (transform.localPosition.y <= (endPos.y + 0.5f))
        {
            isCamUp = false;
        }
    }
    public void MoveCamDown()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, startPos, ref speed, spd);
        if (transform.localPosition.y >= startPos.y - 0.5f)
        {
            isCamDown = false;
        }
    }
    public void SetIsCamUpTrue()
    {
        isCamUp = true;
    }
    public void SetIsCamDownTrue()
    {
        isCamDown = true;
    }
}
