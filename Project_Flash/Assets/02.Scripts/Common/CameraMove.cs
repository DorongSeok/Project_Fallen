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
    public float moveDamping;

    public float waitingTime;

    private void Awake()
    {
        isMoving = false;
        cam = Camera.main;
        camTr = GetComponent<Transform>();
    }
    private void Update()
    {
        if (targetTr != null)
        {
            movePos = (Vector3.up * targetTr.position.y) + (targetTr.forward * camHeight); // ī�޶� �̵� ��, �̵� ������ ��Ÿ���� ��
                                                                                           // ���� 3������ ī�޶� �̵� �� 1���� ������ ������ ��
                                                                                           //camTr.position = Vector3.Slerp(camTr.position, movePos, Time.deltaTime * moveDamping); // õõ�� �������
                                                                                           //camTr.position = movePos; // ��� �������
            if (cam.WorldToScreenPoint(targetTr.position).y > (cam.WorldToScreenPoint(camTr.position).y * 1.6) && isMoving == false) // ��ũ�� �߾� ������ �����ؼ� ȭ���� �Ѿ�� ���� ������ ���̸� ������ ��Ÿ���� 60%�� ���̳�. �׿� ���� ������ 1.6, 0.4
            {
                StartCoroutine(nameof(MoveUpCamera));
            }
            else if (cam.WorldToScreenPoint(targetTr.position).y < (cam.WorldToScreenPoint(camTr.position).y * 0.4) && isMoving == false)
            {
                StartCoroutine(nameof(MoveDownCamera));
            }
        }
        
    }
    private void LateUpdate()
    {
        // PC �̵� ���⿡ ���� ī�޶� �̵�
        if (camUp == true)
        {
            camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.up * upDownDamping, Time.deltaTime * moveDamping);
        }
        if (camDown == true)
        {
            camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.down * upDownDamping, Time.deltaTime * moveDamping);
        }
    }
    IEnumerator MoveUpCamera() // ī�޶� ��½�ų �� ó��
    {
        isMoving = true;
        camUp = true;
        yield return new WaitForSeconds(waitingTime);
        isMoving = false;
        camUp = false;
    }
    IEnumerator MoveDownCamera() // ī�޶� �ϰ���ų �� ó��
    {
        isMoving = true;
        camDown = true;
        yield return new WaitForSeconds(waitingTime);
        isMoving = false;
        camDown = false;
    }

    public void SetTargetTr(Transform targetTr)
    {
        this.targetTr = targetTr;
    }
}
