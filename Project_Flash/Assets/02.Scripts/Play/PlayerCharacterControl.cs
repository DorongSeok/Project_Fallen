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
            Debug.Log("���̺� !!" + collision.gameObject.GetComponentsInChildren<Transform>()[1].name);
            GameManager.instance.SetSavePoint(collision.gameObject.GetComponentsInChildren<Transform>()[1]);
        }
    }
    private void Die()
    {
        Debug.Log("���� ���");
        Destroy(this.gameObject);
        // �״� ���� �Է��� ��

        GameManager.instance.CoroutineStageRestart();
        nonMoveTime = 0;
    }
}
