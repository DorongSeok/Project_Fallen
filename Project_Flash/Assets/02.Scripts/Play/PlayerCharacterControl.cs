using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class PlayerCharacterControl : MonoBehaviour
{
    public delegate void PlayerFallingStart();
    public static event PlayerFallingStart onPlayerFallingStart;

    public delegate void PlayerFallingEnd();
    public static event PlayerFallingEnd onPlayerFallingEnd;

    public GameObject realTimeLight;

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
    public float chargingDamping;

    private Vector3 chargingCircleBase;


    private int fallenCount = 0;

    public float proportionalFactor;

    private bool isignoreLayerCollision = false;
    private bool isMoveStop = false;
    private bool isGameEnd = false;
    private bool isBugCheckerActive = false;

    private float chargeGage = 0.0f;

    public GameObject chargeCore;
    public GameObject chargeEffectObject;
    public GameObject insideCollsionChecker;
    public GameObject bugChecker;
    public GameObject chargingCircle;
    private ParticleSystem chargeEffect;

    private Text text_Height;

    private void Awake()
    {
        // ī�޶� ���� ���� �� ����ó��
        try
        {
            GameObject.FindObjectOfType<CameraMove>().SetPlayer(gameObject);

        }
        catch (NullReferenceException)
        {
            
        }
        
        rigidBody = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();

        fallenCount = Managers.data.GetFallenCount();
        chargeEffect = chargeEffectObject.GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        // ��ǲ �Ŵ��� ����
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        LoadPlayerData();

        chargingCircleBase = chargingCircle.transform.localScale;
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
            Managers.data.SetIsBugCheckerActive(isBugCheckerActive);
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
            this.isBugCheckerActive = Managers.data.GetIsBugCheckerActive();
            if (isignoreLayerCollision == true)
            {
                insideCollsionChecker.SetActive(true);
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
            if (isBugCheckerActive == true)
            {
                bugChecker.SetActive(true);
            }
        }
    }
    void OnKeyboard()
    {
        if (isMoveStop == false)
        {
            if (Input.GetKeyUp(KeyCode.Space) && isMove == false && isFallen == false)
            {
                directionX = Input.GetAxisRaw("Horizontal"); // �¿� �Է�
                directionY = Input.GetAxisRaw("Vertical"); // ���� �Է�
                Move();
            }
            else if (Input.GetKey(KeyCode.Space) && isMove == false && isFallen == false)
            {
                Charging();
            }
        }
    }
    public void SetIsMoveStop(bool isMoveStop)
    {
        if (isMoveStop == true)
        {
            InitializingCharge();
        }
        this.isMoveStop = isMoveStop;
    }
    private void InitializingCharge()
    {
        duration = 0.0f;
        chargeGage = 0.0f;
        chargingCircle.transform.localScale = chargingCircleBase;
        var setParticle = chargeEffect.emission;
        setParticle.rateOverTime = 0.0f;
        chargeCore.SetActive(false);
        chargingCircle.SetActive(false);
    }
    private void Charging() // Ű �Է� ��, �ð��� ���� duration���� ������Ű��, duration���� move�� ������ ��
    {
        duration += Time.deltaTime;
        if (duration >= durationMax) // duration�� �ִ� ���� ������ �ִ밪 ����
        {
            duration = durationMax;
        }
        if (duration > durationMin && chargeGage < 60.0f)
        {
            if (chargeCore.activeSelf == false)
            {
                chargeCore.SetActive(true);
                chargingCircle.SetActive(true);
            }

            chargeGage = 10.0f + (duration * 25.0f);
            var setParticle = chargeEffect.emission;
            setParticle.rateOverTime = chargeGage;
            chargingCircle.transform.localScale = chargingCircleBase * (1 + (duration * chargingDamping)); // �� ũ��� �� ���� ������ ��
        }
    }
    private void Move() // �Է¿� ���� �̵�
    {
        Managers.Sound.Play("Effect/PlayerMove");
        chargeGage = 0.0f;
        var setParticle = chargeEffect.emission;
        setParticle.rateOverTime = 0.0f;
        if (directionX == 0 && directionY == 0) // ������ ���� ��� ��¡ �ʱ�ȭ
        {
            duration = 0;
            chargeCore.SetActive(false);
            chargingCircle.SetActive(false);
            chargingCircle.transform.localScale = chargingCircleBase;
        }
        else
        {
            if (duration <= durationMin)
            {
                if (directionX > 0 && directionY == 0) // ������
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY > 0) // ������ �� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY > 0) // ����
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY > 0) // ���� �� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY == 0) // ����
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY < 0) // ���� �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY < 0) // �Ʒ���
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY < 0) // ������ �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                duration = 0;
            }

            else
            {
                StartCoroutine(nameof(CheckIsStop));
                chargingCircle.SetActive(false);
                chargingCircle.transform.localScale = chargingCircleBase;
                if (directionX > 0 && directionY == 0) // ������
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY > 0) // ������ �� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY > 0) // ����
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY > 0) // ���� �� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY == 0) // ����
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY < 0) // ���� �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY < 0) // �Ʒ���
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY < 0) // ������ �Ʒ� �밢��
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
            }
        }
    }
    IEnumerator CheckIsStop() // ������ ��, ���ǿ� ���� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        // �Է� �ð��� �����ؼ� �� �Է� ���ð��� ���ϴ� ���(�����)

        insideCollsionChecker.SetActive(true);
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
            bool isInsideCollision = insideCollsionChecker.GetComponent<PlayerCharacterInsideCollisionCheck>().GetIsCollision();
            if (isInsideCollision == true)
            {
                if (isFallen == false)
                {
                    Falling();
                }
            }
            else
            {
                insideCollsionChecker.SetActive(false);
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
                bugChecker.SetActive(true);
                isBugCheckerActive = true;
                Falling();
            }
        }
        // ���� ����� ���
        if (collision.gameObject.layer == 7)
        {
            if (isFallen == false)
            {
                bugChecker.SetActive(true);
                isBugCheckerActive = true;
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
        // ��ֹ��� ����� �� ������ �� �κ� �����ؼ� �ϸ� ��
        
        duration = 0;
        fallenCount += 1;

        chargeCore.SetActive(false);
        chargingCircle.SetActive(false);
        var setParticle = chargeEffect.emission;
        setParticle.rateOverTime = 0.0f;

        rigidBody.velocity = Vector3.zero; // ���ڸ��� �ٷ� �߶���
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
        StartCoroutine(nameof(CheckIsFalling));
    }
    IEnumerator CheckIsFalling() // �߷��� ����� ��, ������ �� �߷� ������ ����ϰ� �ٽ� ������ �� �ְ� �ϴ� �ڷ�ƾ
    {
        WaitForEndOfFrame waitflag = new WaitForEndOfFrame();
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
            yield return waitflag;
        }
    }
    private void IsGrounded() // �߷� ���� ����
    {
        onPlayerFallingEnd();
        isFallen = false;
        rigidBody.gravityScale = 0.0f;
        rigidBody.drag = 5.0f;
        nowCheckStopTime = 0.0f;
        if (bugChecker.activeSelf == true)
        {
            bugChecker.SetActive(false);
            isBugCheckerActive = false;
        }
        if (insideCollsionChecker.activeSelf == true)
        {
            insideCollsionChecker.SetActive(false);
            isignoreLayerCollision = false;
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }
    }
    public void InsideCollsionEnd() // ��¡ �̵� ��, ��ֹ��� ��ġ�ؼ� �߶��� ���, �ش� ��ֹ��� ���������� �� ����Ǵ� �Լ�
    {
        if (isignoreLayerCollision == true)
        {
            insideCollsionChecker.SetActive(false);
            isignoreLayerCollision = false;
            Physics2D.IgnoreLayerCollision(6, 8, false);
        }
    }
    public void LightOn()
    {
        realTimeLight.SetActive(true);
    }
    public bool GetIsMove()
    {
        return isMove;
    }

    public int GetFallenCount()
    {
        return fallenCount;
    }
    public bool GetIsFallen()
    {
        return isFallen;
    }
    public void SetIsGameEnd(bool isGameEnd)
    {
        this.isGameEnd = isGameEnd;
    }
    public void SetText_Height(Text text_Height)
    {
        this.text_Height = text_Height;
        StartCoroutine(nameof(DisplayNowHeight));
    }
    IEnumerator DisplayNowHeight()
    {
        WaitForSeconds waitflag = new WaitForSeconds(0.1f);
        while(isGameEnd == false)
        {
            text_Height.text = $"{(int)transform.position.y}m";
            yield return waitflag;
        }
    }
}
