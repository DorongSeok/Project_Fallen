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
        GameObject.FindObjectOfType<CameraMove>().SetTargetTr(this.transform);
        rigidBody = GetComponent<Rigidbody2D>();
        isMove = false;
    }
    private void Start()
    {
        //SetPosition(GameManager.instance.GetSavePoint());

        StartCoroutine(nameof(CheckPCMove));
    }

    //private void SetPosition(Transform pos)
    //{
    //    gameObject.transform.position = pos.position;
    //}
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
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            Debug.Log(collision.gameObject.name);
            Die();
        }
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("세이브 !!" + collision.gameObject.GetComponentsInChildren<Transform>()[1].name);
            GameManager.instance.SetSavePoint(collision.gameObject.GetComponentsInChildren<Transform>()[1]);
        }
    }
    private void Die()
    {
        Debug.Log("으앙 쥬금");
        Destroy(this.gameObject);
        // 죽는 연출 입력할 것

        GameManager.instance.CoroutineStageRestart();
        nonMoveTime = 0;
    }
}
