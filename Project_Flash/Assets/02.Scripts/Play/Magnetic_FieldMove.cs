using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic_FieldMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float moveSpeed;

    public float resetDistance;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartMove();
    }

    public void StartMove()
    {
        rigidBody.AddForce(Vector3.up * moveSpeed, ForceMode2D.Force);
    }

    public void ReSetPosition(Transform resetTr)
    {
        transform.position = resetTr.position + (Vector3.down * resetDistance);
    }
}
