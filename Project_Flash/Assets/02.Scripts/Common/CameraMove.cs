using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Transform camTr;
    private Camera cam;
    private Vector3 movePos;

    private bool isMoving;
    private bool camUp;
    private bool camDown;
    
    private float camHeight = -10.0f;

    public Transform targetTr;

    public float upDownDamping;
    public float downDamping;
    public float moveDamping;
    public float topScreenPoint;
    public float bottonScreenPoint;

    public float waitingTime;

    private void Awake()
    {
        isMoving = false;
        cam = Camera.main;
        camTr = GetComponent<Transform>();
    }
    private void Update()
    {
        Debug.Log(cam.WorldToScreenPoint(targetTr.position).y);
        movePos = (Vector3.up * targetTr.position.y) + (targetTr.forward * camHeight); // 천천히 및 즉시 따라오기에서 사용하는 변수
        //camTr.position = Vector3.Slerp(camTr.position, movePos, Time.deltaTime * moveDamping); // 천천히 따라오기
        //camTr.position = movePos; // 즉시 따라오기
        //Debug.Log(cam.WorldToScreenPoint(targetTr.position).y);
        if (cam.WorldToScreenPoint(targetTr.position).y > topScreenPoint && isMoving == false)
        {
            StartCoroutine(nameof(MoveUpCamera));
        }
        else if (cam.WorldToScreenPoint(targetTr.position).y < bottonScreenPoint && isMoving == false)
        {
            StartCoroutine(nameof(MoveDownCamera));
        }
    }
    private void LateUpdate()
    {
        if (camUp == true)
        {
            camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.up * upDownDamping, Time.deltaTime * moveDamping);
        }
        if (camDown == true)
        {
            camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.down * upDownDamping, Time.deltaTime * moveDamping);
        }
    }
    IEnumerator MoveUpCamera()
    {
        isMoving = true;
        camUp = true;
        yield return new WaitForSeconds(waitingTime);
        isMoving = false;
        camUp = false;
    }
    IEnumerator MoveDownCamera()
    {
        isMoving = true;
        camDown = true;
        yield return new WaitForSeconds(waitingTime);
        isMoving = false;
        camDown = false;
    }
}
