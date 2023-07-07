using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSceneCharacterController : MonoBehaviour
{
    public float startPositionX = -3.01f;
    public float startPositionY = -23.11f;
    public float startPositionZ = 0.0f;
    public float endPositionY = -70.11f;
    public float rotationZSpeed = 1.0f;
    public float maxVelocityY;

    private float rotationZ = 0;

    private bool isShaking = false;
    private Rigidbody2D rbody;

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        CharacterReset();
    }

    void FixedUpdate()
    {
        if (isShaking == true)
        {
            rotationZ += Time.deltaTime * rotationZSpeed * 100.0f;
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
        else
        {
            limitMoveSpeed();
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
    void limitMoveSpeed()
    {
        if (rbody.velocity.y > maxVelocityY)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, maxVelocityY);
        }
        if (rbody.velocity.y < -maxVelocityY)
        {
            rbody.velocity = new Vector2(rbody.velocity.y, -maxVelocityY);
        }
    }
}
