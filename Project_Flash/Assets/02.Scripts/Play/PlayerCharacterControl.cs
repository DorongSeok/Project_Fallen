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
        // 카메라 연동 실패 시 예외처리
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
        // 인풋 매니저 연동
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        LoadPlayerData();

        chargingCircleBase = chargingCircle.transform.localScale;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyboard;
    }
    public void SavePlayerData() // 게임 재 시작 시 불러와야 하는 데이터 저장
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
    public void LoadPlayerData() // 게임 재 시작 시 데이터 불러오기
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
            if (isFallen == true) // 게임 저장 때 추락 중이였다면, 추락을 멈췄음을 확인하는 코루틴 재 실행
            {
                StartCoroutine(nameof(CheckIsFalling));
            }
            if (isMove == true) // 게임 저장 때 이동 중이였다면, 이동을 멈췄음을 확인하는 코루틴 재 실행
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
                directionX = Input.GetAxisRaw("Horizontal"); // 좌우 입력
                directionY = Input.GetAxisRaw("Vertical"); // 상하 입력
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
    private void Charging() // 키 입력 중, 시간에 따라 duration값을 증가시키고, duration값은 move에 영향을 줌
    {
        duration += Time.deltaTime;
        if (duration >= durationMax) // duration이 최대 값을 넘으면 최대값 대입
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
            chargingCircle.transform.localScale = chargingCircleBase * (1 + (duration * chargingDamping)); // 원 크기는 이 구간 조정할 것
        }
    }
    private void Move() // 입력에 따른 이동
    {
        Managers.Sound.Play("Effect/PlayerMove");
        chargeGage = 0.0f;
        var setParticle = chargeEffect.emission;
        setParticle.rateOverTime = 0.0f;
        if (directionX == 0 && directionY == 0) // 방향이 없을 경우 차징 초기화
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
                if (directionX > 0 && directionY == 0) // 오른쪽
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY > 0) // 오른쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY > 0) // 위쪽
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY > 0) // 왼쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY == 0) // 왼쪽
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY < 0) // 왼쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY < 0) // 아래쪽
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY < 0) // 오른쪽 아래 대각선
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
                if (directionX > 0 && directionY == 0) // 오른쪽
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY > 0) // 오른쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY > 0) // 위쪽
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY > 0) // 왼쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY == 0) // 왼쪽
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX < 0 && directionY < 0) // 왼쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY < 0) // 아래쪽
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX > 0 && directionY < 0) // 오른쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
            }
        }
    }
    IEnumerator CheckIsStop() // 움직인 후, 조건에 따라 다시 움직일 수 있게 하는 코루틴
    {
        // 입력 시간에 대응해서 재 입력 대기시간이 변하는 경우(정비례)

        insideCollsionChecker.SetActive(true);
        Managers.data.SetSavePos(gameObject.transform.position);
        isMove = true;
        Physics2D.IgnoreLayerCollision(6, 8, true); 
        isignoreLayerCollision = true;
        delayTime2 = (duration) * 0.6f;
        yield return new WaitForSeconds(delayTime2);

        CheckChargeMoveEnd();
    }
    private void CheckChargeMoveEnd() // 차징 이동 종료 후, 장애물 내부에 위치하는지 판단하는 구간
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
            duration = 0; // 움직인 후 차징 초기화
            chargeCore.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물에 닿았을 경우
        if (collision.gameObject.layer == 8)
        {
            if (isFallen == false)
            {
                bugChecker.SetActive(true);
                isBugCheckerActive = true;
                Falling();
            }
        }
        // 벽에 닿았을 경우
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
    public void Falling() // 중력 적용
    {
        // 장애물에 닿았을 때 판정은 이 부분 수정해서 하면 됨
        
        duration = 0;
        fallenCount += 1;

        chargeCore.SetActive(false);
        chargingCircle.SetActive(false);
        var setParticle = chargeEffect.emission;
        setParticle.rateOverTime = 0.0f;

        rigidBody.velocity = Vector3.zero; // 닿자마자 바로 추락함
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
        StartCoroutine(nameof(CheckIsFalling));
    }
    IEnumerator CheckIsFalling() // 중력이 적용된 후, 멈췄을 때 중력 적용을 취소하고 다시 움직일 수 있게 하는 코루틴
    {
        WaitForEndOfFrame waitflag = new WaitForEndOfFrame();
        onPlayerFallingStart();
        isFallen = true;
        yield return new WaitForSeconds(0.1f);

        // 굴러가지 않고, 중력만을 사용하는 방식에서 채용하는 추락 감지
        // 이 구문 활성화 시, freeze rotation 체크하고, angular drag값을 0.05만 준 후, 각 장애물 오브젝트의 최소 각도를 30도 이상으로 설정해야함
        while (isFallen == true)
        {
            if (rigidBody.velocity == Vector2.zero) // velocity를 통한 추락감지(== 만 사용 가능)
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
    private void IsGrounded() // 중력 적용 해제
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
    public void InsideCollsionEnd() // 차징 이동 후, 장애물에 위치해서 추락할 경우, 해당 장애물을 빠져나왔을 때 실행되는 함수
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
