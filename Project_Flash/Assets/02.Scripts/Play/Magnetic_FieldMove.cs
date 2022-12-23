using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic_FieldMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public float moveSpeed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigidBody.AddForce(Vector3.up * moveSpeed, ForceMode2D.Force);
    }
}
