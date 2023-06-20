using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneCameraController : MonoBehaviour
{
    public float startPositionX = 0.0f;
    public float startPositionY = -22.42f;
    public float startPositionZ = -10.0f;
    public float endPositionY = -77.9f;

    public float startCameraSize = 15;
    public float lastCameraSize = 5;

    private float nowCameraSize = 15;
    public float zoomSpeed = 0.1f;
    private bool isZooming = false;
    private bool isFalling = false;

    void Start()
    {
        CameraReset();
    }

    void FixedUpdate()
    {
        if(nowCameraSize > lastCameraSize && isZooming == true)
        {
            Zoom();
        }
        if(isFalling == true)
        {
            if(this.transform.position.y < endPositionY)
            {
                Vector3 newPosition = new Vector3(startPositionX, endPositionY, startPositionZ);
                this.transform.position = newPosition;
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.GetComponent<Rigidbody2D>().isKinematic = true;
                isFalling = false;
            }
        }
    }

    public void StartZoom()
    {
        isZooming = true;
    }

    private void Zoom()
    {
        float smoothZoomSize = nowCameraSize - (Time.deltaTime * zoomSpeed * 100.0f);
        if (smoothZoomSize <= lastCameraSize)
        {
            smoothZoomSize = lastCameraSize;
        }
        this.GetComponent<Camera>().orthographicSize = smoothZoomSize;
        nowCameraSize = smoothZoomSize;
        
    }
    public void CameraReset()
    {
        Vector3 newPosition = new Vector3(startPositionX, startPositionY, startPositionZ);
        this.transform.position = newPosition;
        isZooming = false;
        isFalling = false;
        nowCameraSize = startCameraSize;
        this.GetComponent<Camera>().orthographicSize = nowCameraSize;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }
    public void StartFalling()
    {
        isFalling = true;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
