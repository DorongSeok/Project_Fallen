using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CircleCollider2D coll;

    private float directionX;
    private float directionY;

    private bool isMove;
    private bool isFallen;


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
        // 카메라 연동 실패 시 예외처리
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
    private void Start()
    {
        LoadPlayerData();
    }
    public void SavePlayerData() // 게임 재 시작 시 불러와야 하는 데이터 저장
    {
        if (DataManager.instance != null)
        {
            DataManager.instance.SetSavePos(gameObject.transform.position);
            DataManager.instance.SetVelocity(rigidBody.velocity);
            DataManager.instance.SetGravityScale(rigidBody.gravityScale);
            DataManager.instance.SetLinearDrag(rigidBody.drag);
            DataManager.instance.SetIsFallen(isFallen);
            DataManager.instance.SetIsMove(isMove);
            DataManager.instance.SetDuration(duration);
        }
    }
    public void LoadPlayerData() // 게임 재 시작 시 데이터 불러오기
    {
        if (DataManager.instance != null)
        {
            gameObject.transform.position = DataManager.instance.GetSavePos();
            rigidBody.velocity = DataManager.instance.GetVelocity();
            rigidBody.gravityScale = DataManager.instance.GetGravityScale();
            rigidBody.drag = DataManager.instance.GetLinearDrag();
            this.isFallen = DataManager.instance.GetIsFallen();
            this.isMove = DataManager.instance.GetIsMove();
            //this.duration = DataManager.instance.GetDuration();

            if (isFallen == true) // 게임 저장 때 추락 중이였다면, 추락을 멈췄음을 확인하는 코루틴 재 실행
            {
                StartCoroutine(nameof(CheckIsFalling));
            }
            if (isMove == true) // 게임 저장 때 이동 중이였다면, 이동을 멈췄음을 확인하는 코루틴 재 실행
            {
                this.duration = DataManager.instance.GetDuration();
                StartCoroutine(nameof(CheckIsStop));
            }
        }
    }
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // 좌우 입력
        directionY = Input.GetAxisRaw("Vertical"); // 상하 입력

        // 떨어지고 있을 때 및 이동 중일 때를 제외한 상황에서만 입력을 받음
        // 이동 전, 충전하는 부분만 isMove가 false일 때도 가능하게 할지 결정할 것
        if (Input.GetKey(KeyCode.Space) && isMove == false && isFallen == false) 
        {
            Charging();
        }
        if (Input.GetKeyUp(KeyCode.Space) && isMove == false && isFallen == false)
        {
            Move();
        }

        // 위치 리셋
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void Charging() // 키 입력 중, 시간에 따라 duration값을 증가시키고, duration값은 move에 영향을 줌
    {
        duration += Time.deltaTime;
    }
    private void Move() // 입력에 따른 이동
    {
        if (directionX == 0 && directionY == 0) // 방향이 없을 경우 차징 초기화
        {
            duration = 0;
        }
        else
        {
            if (duration >= durationMax) // duration이 최대 값을 넘으면 최대값 대입
            {
                duration = durationMax;
            }
            //else if (duration <= durationMin) // duration이 최소 값을 넘지 못하면 최소값 대입
            //{
            //    duration = durationMin;
            //}
            if (duration <= durationMin)
            {
                if (directionX == 1 && directionY == 0) // 오른쪽
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == 1) // 오른쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == 1) // 위쪽
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 1) // 왼쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 0) // 왼쪽
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == -1) // 왼쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == -1) // 아래쪽
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == -1) // 오른쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * durationMin), ForceMode2D.Force);
                }
                duration = 0;
            }

            else
            {
                if (directionX == 1 && directionY == 0) // 오른쪽
                {
                    rigidBody.AddForce((Vector2.right.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == 1) // 오른쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == 1) // 위쪽
                {
                    rigidBody.AddForce((Vector2.up.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 1) // 왼쪽 위 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.up).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == 0) // 왼쪽
                {
                    rigidBody.AddForce((Vector2.left.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == -1 && directionY == -1) // 왼쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.left + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 0 && directionY == -1) // 아래쪽
                {
                    rigidBody.AddForce((Vector2.down.normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                else if (directionX == 1 && directionY == -1) // 오른쪽 아래 대각선
                {
                    rigidBody.AddForce(((Vector2.right + Vector2.down).normalized * moveSpeed * duration), ForceMode2D.Force);
                }
                StartCoroutine(nameof(CheckIsStop));
            }
        }
    }
    IEnumerator CheckIsStop() // 움직인 후, 조건에 따라 다시 움직일 수 있게 하는 코루틴
    {
        //// 움직임을 멈추는 것이 조건인 경우
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

        //// 고정된 시간동안 대기하는 것이 조건인 경우
        //isMove = true;
        //yield return new WaitForSeconds(delayTime);
        //DataManager.instance.SetSavePos(gameObject.transform.position);
        //isMove = false;

        // 입력 시간에 대응해서 재 입력 대기시간이 변하는 경우(짧게 이동 시 길게, 길게 이동시 짧게(반비례))
        //isMove = true;
        //delayTime2 = (proportionalFactor - duration) * 0.5f;
        //yield return new WaitForSeconds(delayTime2);
        //DataManager.instance.SetSavePos(gameObject.transform.position);
        //isMove = false;

        // 입력 시간에 대응해서 재 입력 대기시간이 변하는 경우(정비례)
        isMove = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        isignoreLayerCollision = true;
        delayTime2 = (duration) * 0.6f;
        yield return new WaitForSeconds(delayTime2);
        DataManager.instance.SetSavePos(gameObject.transform.position);
        isMove = false;

        bool isInsideCollision = transform.GetChild(0).gameObject.GetComponent<PlayerCharacterInsideCollisionCheck>().InsideCollisionCheck();
        if(isInsideCollision == true)
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
        duration = 0; // 움직인 후 차징 초기화
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물, 벽에 닿았을 경우 처리
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            if (isFallen == false)
            {
                Falling();
            }
        }
    }
    public void Falling() // 중력 적용
    {
        // 살짝 튕겨났다가 중력 적용되는 연출은 어떨지 생각해볼 것
        // 장애물에 닿았을 때 판정은 이 부분 수정해서 하면 됨

        rigidBody.velocity = Vector3.zero; // 닿자마자 바로 추락함
        rigidBody.gravityScale = 1.0f;
        rigidBody.drag = 0.0f;
        StartCoroutine(nameof(CheckIsFalling));
    }
    IEnumerator CheckIsFalling() // 중력이 적용된 후, 멈췄을 때 중력 적용을 취소하고 다시 움직일 수 있게 하는 코루틴
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
    private void IsGrounded() // 중력 적용 해제
    {
        isFallen = false;
        rigidBody.gravityScale = 0.0f;
        rigidBody.drag = 5.0f;
    }

    private void ResetPosition()
    {
        transform.position = Vector3.zero;
    }

    public void InsideCollsionEnd()
    {
        if (isignoreLayerCollision == true)
        {
            Physics2D.IgnoreLayerCollision(6, 8, false);
            isignoreLayerCollision = false;
        }
    }
}
