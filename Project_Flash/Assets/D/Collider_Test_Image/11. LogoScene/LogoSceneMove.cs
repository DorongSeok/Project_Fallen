using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneMove : MonoBehaviour
{
    public float posX = 0;
    public float startY = 0;
    public float endY = 0;
    public float posZ = 0;

    public GameObject logoSceneCamera;

    private bool isCameraMove = false;
    private Vector3 basePos;

    private void Start()
    {
        basePos = logoSceneCamera.transform.localPosition;
    }

    void Update()
    {
        if(this.transform.position.y <= endY)
        {
            Vector3 newPosition = new Vector3(posX, endY, posZ);
            this.transform.position = newPosition;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        if (isCameraMove == true)
        {
            logoSceneCamera.transform.localPosition = Vector3.Slerp(logoSceneCamera.transform.localPosition, basePos + (Vector3.down * 2.0f), Time.deltaTime * 0.5f);
        }
    }

    public void MoveReset()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        Vector3 newPosition = new Vector3( posX, startY, posZ );
        this.transform.position = newPosition;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().isKinematic = true;

        isCameraMove = false;
    }

    public void MoveStart()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        isCameraMove = true;
    }
}
