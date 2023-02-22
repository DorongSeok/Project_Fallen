using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacterTest2 : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D coll;

    private float directionX;
    private float directionY;

    private bool isMove;
    private bool isFallen;

    private float nowCheckStopTime;

    public float maxCheckStopTime;
    public float stopVelocity;
    public float moveSpeed;
    public float delayTime;
    private float delayTime2;

    private float duration;
    public float durationMax;
    public float durationMin;

    public float proportionalFactor;

    private bool isignoreLayerCollision = false;

    //private int playerLayer, groundLayer, obstacleLayer;

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
        coll = GetComponent<CircleCollider2D>();

        //playerLayer = LayerMask.NameToLayer("Player");
    }
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // �¿� �Է�
        directionY = Input.GetAxisRaw("Vertical"); // ���� �Է�

        // �������� ���� �� �� �̵� ���� ���� ������ ��Ȳ������ �Է��� ����
        // �̵� ��, �����ϴ� �κи� isMove�� false�� ���� �����ϰ� ���� ������ ��
        if (Input.GetKey(KeyCode.Space) && isMove == false && isFallen == false)
        {
            Charging();
        }
        if (Input.GetKeyUp(KeyCode.Space) && isMove == false && isFallen == false)
        {
            Move();
        }

        // ��ġ ����
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
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
        if (directionX == 0 && directionY == 0) // ������ ���� ��� ��¡ �ʱ�ȭ
        {
            duration = 0;
        }
        else
        {
            if (duration >= durationMax) // duration�� �ִ� ���� ������ �ִ밪 ����
            {
                duration = durationMax;
            }
            //else if (duration <= durationMin) // duration�� �ּ� ���� ���� ���ϸ� �ּҰ� ����
            //{
            //    duration = durationMin;
            //}
            if (duration <= durationMin)
            {
                if (directionX == 1 && directionY == 0) // ������
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == 1) // ������ �� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == 1) // ����
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 1) // ���� �� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 0) // ����
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == -1) // ���� �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == -1) // �Ʒ���
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == -1) // ������ �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
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
                StartCoroutine(nameof(CheckIsStop));
            }
        }
    }
    IEnumerator CheckIsStop() // ������ ��, ���ǿ� ���� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        //// �������� ���ߴ� ���� ������ ���
        //isMove = true;
        //yield return new WaitForSeconds(0.1f);
        //while(isMove == true)
        //{
        //    if (rigidBody.velocity.magnitude <= stopVelocity)
        //    {
        //        if (DataManager.instance != null)
        //        {
        //            DataManager.instance.SetSavePos(gameObject.transform.position);
        //        }
        //        isMove = false;
        //    }
        //    yield return new WaitForEndOfFrame();
        //}

        //// ������ �ð����� ����ϴ� ���� ������ ���
        //isMove = true;
        //yield return new WaitForSeconds(delayTime);
        //DataManager.instance.SetSavePos(gameObject.transform.position);
        //isMove = false;

        // �Է� �ð��� �����ؼ� �� �Է� ���ð��� ���ϴ� ���(ª�� �̵� �� ���, ��� �̵��� ª��(�ݺ��))
        //isMove = true;
        //delayTime2 = (proportionalFactor - duration) * 0.5f;
        //yield return new WaitForSeconds(delayTime2);
        //DataManager.instance.SetSavePos(gameObject.transform.position);
        //isMove = false;

        // �Է� �ð��� �����ؼ� �� �Է� ���ð��� ���ϴ� ���(�����)
        isMove = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        isignoreLayerCollision = true;
        delayTime2 = (duration) * 0.6f;
        yield return new WaitForSeconds(delayTime2);
        isMove = false;

        bool isInsideCollision = transform.GetChild(0).gameObject.GetComponent<PlayerCharacterInsideCollisionCheckTest>().GetIsCollision();
        if (isInsideCollision == true)
        {
            if (isFallen == false)
            {
                Falling();
            }
        }
        else
        {
            Physics2D.IgnoreLayerCollision(6, 8, false);
            isignoreLayerCollision = false;
        }
        duration = 0; // ������ �� ��¡ �ʱ�ȭ
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ�, ���� ����� ��� ó��
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            if (isFallen == false)
            {
                Falling();
            }
        }
    }
    public void Falling() // �߷� ����
    {
        // ��¦ ƨ�ܳ��ٰ� �߷� ����Ǵ� ������ ��� �����غ� ��
        // ��ֹ��� ����� �� ������ �� �κ� �����ؼ� �ϸ� ��

        duration = 0;

        rigidBody.velocity = Vector3.zero; // ���ڸ��� �ٷ� �߶���
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
        StartCoroutine(nameof(CheckIsFalling));
    }
    IEnumerator CheckIsFalling() // �߷��� ����� ��, ������ �� �߷� ������ ����ϰ� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        isFallen = true;
        yield return new WaitForSeconds(0.1f);

        // �������� �ʰ�, �߷¸��� ����ϴ� ��Ŀ��� ä���ϴ� �߶� ����
        // �� ���� Ȱ��ȭ ��, freeze rotation üũ�ϰ�, angular drag���� 0.05�� �� ��, �� ��ֹ� ������Ʈ�� �ּ� ������ 30�� �̻����� �����ؾ���
        while (isFallen == true)
        {
            if (rigidBody.velocity == Vector2.zero) // velocity�� ���� �߶�����(== �� ��� ����)
            {
                if (nowCheckStopTime < maxCheckStopTime)
                {
                    nowCheckStopTime += Time.deltaTime;
                }
                else
                {
                    if (DataManager.instance != null)
                    {
                        DataManager.instance.SetSavePos(gameObject.transform.position);
                    }
                    IsGrounded();
                }
            }
            else if (rigidBody.velocity != Vector2.zero && nowCheckStopTime != 0.0f)
            {
                nowCheckStopTime = 0.0f;
            }
            yield return new WaitForEndOfFrame();
        }

        // �߷°� �������� ����� ���ÿ� ä���ϴ� �߶� ����
        // �� ���� Ȱ��ȭ ��, freeze rotation ��üũ�ϰ�, angular drag���� 5�� �� ��, �� ��ֹ� ������Ʈ�� �ּ� ������ 25�� �̻����� �����ؾ� ��
        //while (isFallen == true)
        //{
        //    if (rigidBody.velocity.magnitude <= stopVelocity)
        //    {
        //        if (nowCheckStopTime < maxCheckStopTime)
        //        {
        //            nowCheckStopTime += Time.deltaTime;
        //        }
        //        else
        //        {
        //            if (DataManager.instance != null)
        //            {
        //                DataManager.instance.SetSavePos(gameObject.transform.position);
        //            }
        //            IsGrounded();
        //        }
        //    }
        //    else if (rigidBody.velocity.magnitude > stopVelocity && nowCheckStopTime != 0.0f)
        //    {
        //        nowCheckStopTime = 0.0f;
        //    }
        //    yield return new WaitForEndOfFrame();
        //}
    }
    private void IsGrounded() // �߷� ���� ����
    {
        isFallen = false;
        rigidBody.gravityScale = 0.0f;
        rigidBody.drag = 5.0f;
        nowCheckStopTime = 0.0f;
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void InsideCollsionEnd()
    {
        Debug.Log("�λ��̵� �ݸ������� ����");
        if (isignoreLayerCollision == true)
        {
            Physics2D.IgnoreLayerCollision(6, 8, false);
            isignoreLayerCollision = false;
        }
    }
    public bool GetIsMove()
    {
        return isMove;
    }
}
