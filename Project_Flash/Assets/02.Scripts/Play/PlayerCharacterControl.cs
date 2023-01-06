using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float directionX;
    private float directionY;

    private bool isMove;
    private bool isFallen;


    public float stopVelocity;
    public float moveSpeed;
    public float delayTime;

    private float duration;
    public float durationMax;
    public float durationMin;

    private void Awake()
    {
        // ī�޶� ���� ���� �� ����ó��
        try
        {
            GameObject.FindObjectOfType<CameraMove>().SetTargetTr(this.transform);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Camera is Null");
        }
        
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        LoadPlayerData();
    }
    public void SavePlayerData() // ���� �� ���� �� �ҷ��;� �ϴ� ������ ����
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.SetSavePos(gameObject.transform.position);
            DataManager.instance.SetVelocity(rigidBody.velocity);
            DataManager.instance.SetGravityScale(rigidBody.gravityScale);
            DataManager.instance.SetLinearDrag(rigidBody.drag);
            DataManager.instance.SetIsFallen(isFallen);
            DataManager.instance.SetIsMove(isMove);
        }
    }
    public void LoadPlayerData() // ���� �� ���� �� ������ �ҷ�����
    {
        if (DataManager.instance != null)
        {
            gameObject.transform.position = DataManager.instance.GetSavePos();
            rigidBody.velocity = DataManager.instance.GetVelocity();
            rigidBody.gravityScale = DataManager.instance.GetGravityScale();
            rigidBody.drag = DataManager.instance.GetLinearDrag();
            this.isFallen = DataManager.instance.GetIsFallen();
            this.isMove = DataManager.instance.GetIsMove();
            if (isFallen == true) // ���� ���� �� �߶� ���̿��ٸ�, �߶��� �������� Ȯ���ϴ� �ڷ�ƾ �� ����
            {
                StartCoroutine(nameof(CheckIsFalling));
            }
            if (isMove == true) // ���� ���� �� �̵� ���̿��ٸ�, �̵��� �������� Ȯ���ϴ� �ڷ�ƾ �� ����
            {
                StartCoroutine(nameof(CheckIsStop));
            }
        }
    }
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // �¿� �Է�
        directionY = Input.GetAxisRaw("Vertical"); // ���� �Է�

        // �������� ���� �� �� �̵� ���� ���� ������ ��Ȳ������ �Է��� ����
        if (Input.GetKey(KeyCode.Space) && isMove == false && isFallen == false) 
        {
            Charging();
        }
        if (Input.GetKeyUp(KeyCode.Space) && isMove == false && isFallen == false)
        {
            Move();
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void Charging() // Ű �Է� ��, �ð��� ���� duration���� ������Ű��, duration���� move�� ������ ��
    {
        duration += Time.deltaTime;
    }
    private void Move() // �Է¿� ���� �̵�
    {
        if (duration >= durationMax) // duration�� �ִ� ���� ������ �ִ밪 ����
        {
            duration = durationMax;
        }
        else if (duration <= durationMin) // duration�� �ּ� ���� ���� ���ϸ� �ּҰ� ����
        {
            duration = durationMin;
        }
        if (directionX == 0 && directionY == 0) // ������ ���� ��� ��¡ �ʱ�ȭ
        {
            duration = 0;
        }
        else
        {
            if (directionX == 1 && directionY == 0) // ������
            {
                rigidBody.AddForce((Vector2.right.normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == 1) // ������ �� �밢��
            {
                rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == 1) // ����
            {
                rigidBody.AddForce((Vector2.up.normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 1) // ���� �� �밢��
            {
                rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == 0) // ����
            {
                rigidBody.AddForce((Vector2.left.normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == -1 && directionY == -1) // ���� �Ʒ� �밢��
            {
                rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == 0 && directionY == -1) // �Ʒ���
            {
                rigidBody.AddForce((Vector2.down.normalized * moveSpeed * duration), ForceMode2D.Force);
            }
            else if (directionX == 1 && directionY == -1) // ������ �Ʒ� �밢��
            {
                rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
            }
        }
        duration = 0; // ������ �� ��¡ �ʱ�ȭ
        StartCoroutine(nameof(CheckIsStop));
    }
    IEnumerator CheckIsStop() // ������ ��, �������� �����ߴ��� Ȯ���ϰ� �������� ��� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        isMove = true;
        yield return new WaitForSeconds(0.1f);
        while(isMove == true)
        {
            if (rigidBody.velocity.magnitude <= stopVelocity)
            {
                if (DataManager.instance != null)
                {
                    DataManager.instance.SetSavePos(gameObject.transform.position);
                }
                isMove = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ�, ���� ����� ��� ó��
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            if (isFallen == false)
            {
                Falling();
                StartCoroutine(nameof(CheckIsFalling));
            }
        }
    }
    private void Falling() // �߷� ����
    {
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
    }
    IEnumerator CheckIsFalling() // �߷��� ����� ��, ������ �� �߷� ������ ����ϰ� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        isFallen = true;
        yield return new WaitForSeconds(0.1f);
        while (isFallen == true)
        {
            if (rigidBody.velocity.magnitude <= stopVelocity)
            {
                if (DataManager.instance != null)
                {
                    DataManager.instance.SetSavePos(gameObject.transform.position);
                }
                IsGrounded();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void IsGrounded() // �߷� ���� ����
    {
        isFallen = false;
        rigidBody.gravityScale = 0.0f;
        rigidBody.drag = 10.0f;
    }
}
