using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Transform camTr;
    private Transform targetTr;
    //private Camera cam;
    //private Vector3 movePos; // 움직일 목표 위치

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
            // 추후 3가지의 카메라 이동 중 1개를 선택해 적용할 것


            //movePos = (Vector3.up * targetTr.position.y) + (targetTr.forward * camHeight); // 3. 고정 지점 통과 시, 따라오기
            //if (cam.WorldToScreenPoint(targetTr.position).y > (cam.WorldToScreenPoint(camTr.position).y * 1.6) && isMoving == false) // 스크린 중앙 점으로 시작해서 화면이 넘어가는 공간 까지의 차이를 비율로 나타내면 60%씩 차이남. 그에 따른 계산식이 1.6, 0.4
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
        //camTr.position = targetTr.position + (targetTr.forward * camHeight); // 1. 즉시 따라오기
        
        // 3번과 연계한 PC 이동 방향에 따른 카메라 이동
        //if (camUp == true)
        //{
        //    camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.up * upDownDamping, Time.deltaTime * moveDamping);
        //}
        //if (camDown == true)
        //{
        //    camTr.position = Vector3.Slerp(camTr.position, movePos + Vector3.down * upDownDamping, Time.deltaTime * moveDamping);
        //}
    }
    // 3번과 연계한 카메라 이동
    //IEnumerator MoveUpCamera() // 카메라 상승시킬 때 처리
    //{
    //    isMoving = true;
    //    camUp = true;
    //    yield return new WaitForSeconds(waitingTime);
    //    isMoving = false;
    //    camUp = false;
    //}
    //IEnumerator MoveDownCamera() // 카메라 하강시킬 때 처리
    //{
    //    isMoving = true;
    //    camDown = true;
    //    yield return new WaitForSeconds(waitingTime);
    //    isMoving = false;
    //    camDown = false;
    //}
    private void FixedUpdate()
    {
        camTr.position = Vector3.Lerp(camTr.position, new Vector3(0, targetTr.position.y, camHeight), Time.deltaTime * moveDamping); // 2. 천천히 따라오기
    }
    public void SetTargetTr(Transform targetTr) // 카메라가 추적할 대상 세팅
    {
        this.targetTr = targetTr;
        SetCameraStartPos();
    }
    public void SetCameraStartPos()
    {
        camTr.position = new Vector3(camTr.position.x, targetTr.position.y, camTr.position.z);
    }
}
