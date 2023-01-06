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

    public void StartMove() // rigidbody를 가진 자기장 오브젝트에 속력을 줌
    {
        rigidBody.AddForce(Vector3.up * moveSpeed, ForceMode2D.Force);
    }

    public void ReSetPosition(Transform resetTr) // 캐릭터 부활 시, 캐릭터의 위치에 따라 자기장 위치도 초기화 함
    {
        transform.position = resetTr.position + (Vector3.down * resetDistance);
    }
}
