using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Transform camTr;
    private Transform targetTr;
    //private Camera cam;
    //private Vector3 movePos; // ������ ��ǥ ��ġ

    //private bool isMoving;
    //private bool camUp;
    //private bool camDown;
    
    public float camHeight = -10.0f;

    public float upDownDamping;
    public float moveDamping;

    public float waitingTime;

    private void Awake()
    {
        //isMoving = false;
        //cam = Camera.main;
        camTr = GetComponent<Transform>();
    }
    private void Update()
    {
        if (targetTr != null)
        {
            // ���� 3������ ī�޶� �̵� �� 1���� ������ ������ ��


            //movePos = (Vector3.up * targetTr.position.y) + (targetTr.forward * camHeight); // 3. ���� ���� ��� ��, �������
            //if (cam.WorldToScreenPoint(targetTr.position).y > (cam.WorldToScreenPoint(camTr.position).y * 1.6) && isMoving == false) // ��ũ�� �߾� ������ �����ؼ� ȭ���� �Ѿ�� ���� ������ ���̸� ������ ��Ÿ���� 60%�� ���̳�. �׿� ���� ������ 1.6, 0.4
            //{
            //    StartCoroutine(nameof(MoveUpCamera));
            //}
            //else if (cam.WorldToScreenPoint(targetTr.position).y < (cam.WorldToScreenPoint(camTr.position).y * 0.4) && isMoving == false)
            //{
            //    StartCoroutine(nameof(MoveDownCamera));
            //}
        }
        
    }
    private void LateUpdate()
    {
        //camTr.position = targetTr.position + (targetTr.forward * camHeight); // 1. ��� �������
        
        // 3���� ������ PC �̵� ���⿡ ���� ī�޶� �̵�
        //if (camUp == true)
        //{
        //    camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.up * upDownDamping, Time.deltaTime * moveDamping);
        //}
        //if (camDown == true)
        //{
        //    camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.down * upDownDamping, Time.deltaTime * moveDamping);
        //}
    }
    // 3���� ������ ī�޶� �̵�
    //IEnumerator MoveUpCamera() // ī�޶� ��½�ų �� ó��
    //{
    //    isMoving = true;
    //    camUp = true;
    //    yield return new WaitForSeconds(waitingTime);
    //    isMoving = false;
    //    camUp = false;
    //}
    //IEnumerator MoveDownCamera() // ī�޶� �ϰ���ų �� ó��
    //{
    //    isMoving = true;
    //    camDown = true;
    //    yield return new WaitForSeconds(waitingTime);
    //    isMoving = false;
    //    camDown = false;
    //}
    private void FixedUpdate()
    {
        camTr.position = Vector3.Lerp(camTr.position, new Vector3(0, targetTr.position.y, camHeight), Time.deltaTime * moveDamping); // 2. õõ�� �������
    }
    public void SetTargetTr(Transform targetTr) // ī�޶� ������ ��� ����
    {
        this.targetTr = targetTr;
        SetCameraStartPos();
    }
    public void SetCameraStartPos()
    {
        camTr.position = new Vector3(camTr.position.x, targetTr.position.y, camTr.position.z);
    }
}
