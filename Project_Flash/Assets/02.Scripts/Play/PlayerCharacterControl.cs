using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float directionX;
    private float directionY;
    private bool isMove;
    private bool isSafe;
    private float nonMoveTime;

    public float limitTime;

    public float moveSpeed;
    public float delayTime;


    private void Awake()
    {
        isMove = false;
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        StartCoroutine(nameof(CheckPCMove));
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
        nonMoveTime = 0;
        isMove = true;
        yield return new WaitForSeconds(delayTime);
        isMove = false;
    }
    IEnumerator CheckPCMove()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            if (isMove == false && isSafe == false)
            {
                nonMoveTime += Time.deltaTime;
                if (nonMoveTime >= limitTime)
                {
                    Die();
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 9)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("으앙 쥬금");
        this.gameObject.SetActive(false);
        // 이 부분에 일정 시간 후 세이브 지점에서 살아나는 명령 입력
        nonMoveTime = 0;
    }
}
