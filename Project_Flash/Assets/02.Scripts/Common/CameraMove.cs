using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    private Transform camTr;
    private Transform targetTr;
    private GameObject player;
    private PlayerCharacterControl playerCharacterControl;

    private bool isCameraCtrl = false;
    private bool isFalling = false;
    private float directionY;

    public float camHeight = -10.0f;

    public float moveDamping = 10.0f;

    public float waitingTime = 0.5f;

    private float downPoint = 20.0f;
    private float downDamping = 10.0f;
    public float upPoint = 50.0f;
    public float upDamping = 10.0f;

    private void Awake()
    {
        camTr = GetComponent<Transform>();
    }
    private void Start()
    {
        Camera.main.aspect = 16f / 9f;
        PlayerCharacterControl.onPlayerFallingStart += this.StartFalling;
        PlayerCharacterControl.onPlayerFallingEnd += this.EndFalling;
    }
    private void OnDisable()
    {
        PlayerCharacterControl.onPlayerFallingStart -= this.StartFalling;
        PlayerCharacterControl.onPlayerFallingEnd -= this.EndFalling;
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (isCameraCtrl == false)
                {
                    isCameraCtrl = true;
                    playerCharacterControl.SetIsMoveStop(true);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (isCameraCtrl == true)
                {
                    isCameraCtrl = false;
                    playerCharacterControl.SetIsMoveStop(false);
                }
            }
            if (isCameraCtrl == true)
            {
                directionY = Input.GetAxisRaw("Vertical");
            }
        }
    }
    private void FixedUpdate()
    {
        if (isCameraCtrl == true && isFalling == false)
        {
            if (camTr.position.y < 1590.0f && directionY > 0)
            {
                camTr.position = Vector3.MoveTowards(camTr.position, new Vector3(0, targetTr.position.y + upPoint, camHeight), Time.deltaTime * upDamping);
            }
            else if (camTr.position.y > 0 && directionY < 0)
            {
                camTr.position = Vector3.MoveTowards(camTr.position, new Vector3(0, targetTr.position.y - downPoint, camHeight), Time.deltaTime * downDamping);
            }
        }
        else
        {
            camTr.position = Vector3.Lerp(camTr.position, new Vector3(0, targetTr.position.y, camHeight), Time.deltaTime * moveDamping); // 2. 천천히 따라오기
        }
    }
    public void SetIsCameraCtrlFalse()
    {
        isCameraCtrl = false;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
        playerCharacterControl = player.GetComponent<PlayerCharacterControl>();
        SetCameraStartPos();
    }
    public void SetCameraStartPos()
    {
        targetTr = player.transform;
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
