using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCharacterMove : MonoBehaviour
{
    private Rigidbody2D rbody;

    public float spd;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rbody.AddForce(Vector2.up * spd, ForceMode2D.Force);
    }
}
