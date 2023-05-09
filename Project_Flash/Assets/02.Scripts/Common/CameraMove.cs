using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Transform camTr;
    private Transform targetTr;

    private bool isCameraUp = false;
    private bool isFalling = false;
    
    public float camHeight = -10.0f;

    // public float upDownDamping;
    public float moveDamping;

    public float waitingTime;

    public float upPoint;
    public float upDamping;

    private void Awake()
    {
        camTr = GetComponent<Transform>();
    }
    private void Start()
    {
        PlayerCharacterControl.onPlayerFallingStart += this.StartFalling;
        PlayerCharacterControl.onPlayerFallingEnd += this.EndFalling;

        PlayerCharacterTest2.onPlayerFallingStart += this.StartFalling;
        PlayerCharacterTest2.onPlayerFallingEnd += this.EndFalling;
    }
    private void OnDisable()
    {
        PlayerCharacterControl.onPlayerFallingStart -= this.StartFalling;
        PlayerCharacterControl.onPlayerFallingEnd -= this.EndFalling;

        PlayerCharacterTest2.onPlayerFallingStart -= this.StartFalling;
        PlayerCharacterTest2.onPlayerFallingEnd -= this.EndFalling;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            isCameraUp = true;
        }
        else
        {
            isCameraUp = false;
        }
    }
    private void FixedUpdate()
    {
        if (isCameraUp == true && isFalling == false)
        {
            camTr.position = Vector3.Slerp(camTr.position, new Vector3(0, targetTr.position.y + upPoint, camHeight), Time.deltaTime * upDamping);
        }
        else
        {
            camTr.position = Vector3.Lerp(camTr.position, new Vector3(0, targetTr.position.y, camHeight), Time.deltaTime * moveDamping); // 2. 천천히 따라오기
        }
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
    private void StartFalling()
    {
        isFalling = true;
    }
    private void EndFalling()
    {
        isFalling = false;
    }
}
