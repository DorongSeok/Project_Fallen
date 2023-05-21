using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerCharacterControl : MonoBehaviour
{
    public delegate void PlayerFallingStart();
    public static event PlayerFallingStart onPlayerFallingStart;

    public delegate void PlayerFallingEnd();
    public static event PlayerFallingEnd onPlayerFallingEnd;

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


    private int fallenCount = 0;

    public float proportionalFactor;

    private bool isignoreLayerCollision = false;
    private bool isGameStop = false;

    //private PlayerCharacterInsideCollisionCheck playerCharacterInsideCollisionCheck;

    public GameObject chargeCore;
    public VisualEffect chargeEffect;

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

        fallenCount = Managers.data.GetFallenCount();
        //playerCharacterInsideCollisionCheck = GetComponentInChildren<PlayerCharacterInsideCollisionCheck>();


        //playerLayer = LayerMask.NameToLayer("Player");
    }
    private void Start()
    {
        // ��ǲ �Ŵ��� ����
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        LoadPlayerData();
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyboard;
    }
    public void SavePlayerData() // ���� �� ���� �� �ҷ��;� �ϴ� ������ ����
    {
        if (Managers.data != null)
        {
            Managers.data.SetSavePos(gameObject.transform.position);
            Managers.data.SetVelocity(rigidBody.velocity);
            Managers.data.SetGravityScale(rigidBody.gravityScale);
            Managers.data.SetLinearDrag(rigidBody.drag);
            Managers.data.SetIsFallen(isFallen);
            Managers.data.SetIsMove(isMove);
            Managers.data.SetDuration(duration);
            Managers.data.SetIsignoreLayerCollision(isignoreLayerCollision);
            Managers.data.SetFallenCount(fallenCount);
        }
    }
    public void LoadPlayerData() // ���� �� ���� �� ������ �ҷ�����
    {
        if (Managers.data != null)
        {
            gameObject.transform.position = Managers.data.GetSavePos();
            rigidBody.velocity = Managers.data.GetVelocity();
            rigidBody.gravityScale = Managers.data.GetGravityScale();
            rigidBody.drag = Managers.data.GetLinearDrag();
            this.isFallen = Managers.data.GetIsFallen();
            this.isMove = Managers.data.GetIsMove();
            this.isignoreLayerCollision = Managers.data.GetIsignoreLayerCollision();
            if (isignoreLayerCollision == true)
            {
                Physics2D.IgnoreLayerCollision(6, 8, true);
            }
            if (isFallen == true) // ���� ���� �� �߶� ���̿��ٸ�, �߶��� �������� Ȯ���ϴ� �ڷ�ƾ �� ����
            {
                StartCoroutine(nameof(CheckIsFalling));
            }
            if (isMove == true) // ���� ���� �� �̵� ���̿��ٸ�, �̵��� �������� Ȯ���ϴ� �ڷ�ƾ �� ����
            {
                this.duration = Managers.data.GetDuration();
                StartCoroutine(nameof(CheckIsStop));
            }

        }
    }
    void OnKeyboard()
    {
        if (isGameStop == false)
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
        }
        
    }
    public void SetIsGameStop(bool isGameStop)
    {
        this.isGameStop = isGameStop;
    }
    private void FixedUpdate()
    {
        
    }
    private void Charging() // Ű �Է� ��, �ð��� ���� duration���� ������Ű��, duration���� move�� ������ ��
    {
        duration += Time.deltaTime;
        if (chargeCore.activeSelf == false && duration >= durationMin)
        {
            chargeCore.SetActive(true);
        }
        if (duration > 0.4f)
        {
            float chargeGage;
            chargeGage = 0.3f + (duration * 0.5f);
            if (chargeGage > 1.0f)
            {
                chargeGage = 1.0f;
            }
            chargeEffect.SetFloat("ChargeGage", chargeGage);
        }
    }
    private void Move() // �Է¿� ���� �̵�
    {
        chargeEffect.SetFloat("ChargeGage", 0.0f);
        if (directionX == 0 && directionY == 0) // ������ ���� ��� ��¡ �ʱ�ȭ
        {
            duration = 0;
            chargeCore.SetActive(false);
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

        Managers.data.SetSavePos(gameObject.transform.position);
        isMove = true;
        Physics2D.IgnoreLayerCollision(6, 8, true); 
        isignoreLayerCollision = true;
        delayTime2 = (duration) * 0.6f;
        yield return new WaitForSeconds(delayTime2);

        CheckChargeMoveEnd();
    }
    private void CheckChargeMoveEnd() // ��¡ �̵� ���� ��, ��ֹ� ���ο� ��ġ�ϴ��� �Ǵ��ϴ� ����
    {
        if (isMove == true)
        {
            isMove = false;
            bool isInsideCollision = transform.GetChild(0).gameObject.GetComponent<PlayerCharacterInsideCollisionCheck>().GetIsCollision();
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
            chargeCore.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ֹ��� ����� ���
        if (collision.gameObject.layer == 8)
        {
            if (isFallen == false)
            {
                Falling();
            }
        }
        // ���� ����� ���
        if (collision.gameObject.layer == 7)
        {
            if (isFallen == false)
            {
                Falling();
            }
            if (isignoreLayerCollision == true)
            {
                CheckChargeMoveEnd();
            }
        }
    }
    public void Falling() // �߷� ����
    {
        // ��¦ ƨ�ܳ��ٰ� �߷� ����Ǵ� ������ ��� �����غ� ��
        // ��ֹ��� ����� �� ������ �� �κ� �����ؼ� �ϸ� ��
        
        duration = 0;
        fallenCount += 1;

        chargeCore.SetActive(false);
        chargeEffect.SetFloat("ChargeGage", 0);

        rigidBody.velocity = Vector3.zero; // ���ڸ��� �ٷ� �߶���
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
        StartCoroutine(nameof(CheckIsFalling));
    }
    IEnumerator CheckIsFalling() // �߷��� ����� ��, ������ �� �߷� ������ ����ϰ� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        onPlayerFallingStart();
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
                    if (Managers.data != null)
                    {
                        Managers.data.SetSavePos(gameObject.transform.position);
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
        onPlayerFallingEnd();
        isFallen = false;
        rigidBody.gravityScale = 0.0f;
        rigidBody.drag = 5.0f;
        nowCheckStopTime = 0.0f;
    }

    //private void ResetPosition()
    //{
    //    transform.position = Vector3.zero;
    //}

    public void InsideCollsionEnd() // ��¡ �̵� ��, ��ֹ��� ��ġ�ؼ� �߶��� ���, �ش� ��ֹ��� ���������� �� ����Ǵ� �Լ�
    {
        if (isignoreLayerCollision == true)
        {
            isignoreLayerCollision = false;
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }
    }
    public bool GetIsMove()
    {
        return isMove;
    }

    public int GetFallenCount()
    {
        return fallenCount;
    }
}
