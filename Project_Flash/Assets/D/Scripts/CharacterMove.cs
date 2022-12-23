using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float directionX;
    private float directionY;
    private bool isMove;

    public float moveSpeed = 1500;
    public float delayTime = 0.2f;

    private void Awake()
    {
        isMove = false;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // �¿� �Է�
        directionY = Input.GetAxisRaw("Vertical"); // ���� �Է�

        if (Input.GetKeyDown(KeyCode.Space) && isMove == false)
        {
            Move();
        }
    }
    private void Move()
    {
        if (directionX == 0 && directionY == 0) // ������ �� �밢��
        {
            return;
        }
        else
        {
            StartCoroutine(nameof(MoveDelay));
            if (directionX == 1 && directionY == 0) // ������
            {
                rigidBody.AddForce((Vector2.right.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == 1) // ������ �� �밢��
            {
                rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == 1) // ����
            {
                rigidBody.AddForce((Vector2.up.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 1) // ���� �� �밢��
            {
                rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 0) // ����
            {
                rigidBody.AddForce((Vector2.left.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == -1) // ���� �Ʒ� �밢��
            {
                rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == -1) // �Ʒ���
            {
                rigidBody.AddForce((Vector2.down.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == -1) // ������ �Ʒ� �밢��
            {
                rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed), ForceMode2D.Force);
            }
        }
    }
    IEnumerator MoveDelay()
    {
        isMove = true;
        yield return new WaitForSeconds(delayTime);
        isMove = false;
    }
}
