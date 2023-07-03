using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCharacterMove : MonoBehaviour
{
    private Rigidbody2D rbody;
    private bool isMove = false;

    public float spd = 10.0f;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isMove == true)
        {
            rbody.AddForce(Vector2.up * spd, ForceMode2D.Force);
        }
    }
    public void SetIsMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
