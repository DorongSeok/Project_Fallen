using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneCharacterController : MonoBehaviour
{
    public float startPositionX = -3.01f;
    public float startPositionY = -23.11f;
    public float startPositionZ = 0.0f;
    public float endPositionY = -70.11f;

    private float rotationZ = 0;
    public float rotationZSpeed = 1.0f;

    private bool isShaking = false;

    void Start()
    {
        CharacterReset();
    }

    void Update()
    {
        if (isShaking == true)
        {
            rotationZ += Time.deltaTime * rotationZSpeed * 100.0f;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    public void CharacterReset()
    {
        Vector3 newPosition = new Vector3(startPositionX, startPositionY, startPositionZ);
        this.transform.position = newPosition;
        isShaking = false;
        rotationZ = 0;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public void StartShake()
    {
        isShaking = true;
    }
    public void StopShake()
    {
        isShaking = false;
    }
    public void StartFalling()
    {
        rotationZ = 0;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
