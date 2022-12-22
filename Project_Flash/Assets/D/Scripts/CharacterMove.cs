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
        directionX = Input.GetAxisRaw("Horizontal"); // 좌우 입력
        directionY = Input.GetAxisRaw("Vertical"); // 상하 입력

        if (Input.GetKeyDown(KeyCode.Space) && isMove == false)
        {
            Move();
        }
    }
    private void Move()
    {
        if (directionX == 0 && directionY == 0) // 오른쪽 위 대각선
        {
            return;
        }
        else
        {
            StartCoroutine(nameof(MoveDelay));
            if (directionX == 1 && directionY == 0) // 오른쪽
            {
                rigidBody.AddForce((Vector2.right.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == 1) // 오른쪽 위 대각선
            {
                rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == 1) // 위쪽
            {
                rigidBody.AddForce((Vector2.up.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 1) // 왼쪽 위 대각선
            {
                rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 0) // 왼쪽
            {
                rigidBody.AddForce((Vector2.left.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == -1) // 왼쪽 아래 대각선
            {
                rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == -1) // 아래쪽
            {
                rigidBody.AddForce((Vector2.down.normalized * moveSpeed), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == -1) // 오른쪽 아래 대각선
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
