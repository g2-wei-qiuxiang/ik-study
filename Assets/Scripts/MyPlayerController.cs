using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// プレイヤーの行動を制御
/// </summary>
public class MyPlayerController : MonoBehaviour
{
    [Header("キャラクター")] 
    [SerializeField] 
    private Transform character;   // キャラクター

    [SerializeField] 
    private Animator characterAnimator;

    [Header("移動、回転ステータス")]
    [SerializeField] 
    private Transform moveRotateObject;   // 移動オブジェクト
    
    [SerializeField] 
    private Rigidbody moveRotateObjectRigidbody;
    
    [SerializeField] 
    private float playerMoveForce = 0.2f;

    [SerializeField] 
    private float rotateSpeedHorizon = 0.2f;
    
    [SerializeField] 
    private float rotateSpeedVertical = 0.2f;
    
    [SerializeField, Range(-80, 0)] 
    private float rotateVerticalMin = -30f;
    
    [SerializeField, Range(0, 80)] 
    private float rotateVerticalMax = 30f;

    [SerializeField] 
    private Transform verticalRotationObject;
    
    [SerializeField] 
    private float characterRotateInterval = 0.2f;   // キャラの回転インターバル

    private Vector3 characterFaceDirection = Vector3.zero;
    private Vector3 moveForce = Vector3.zero;
    private Vector3 mouseLastPosition = Vector3.one;

    // Start is called before the first frame update
    private void Start()
    {
        characterFaceDirection = moveRotateObject.forward;
    }

    private void FixedUpdate()
    {
        // 移動処理
        moveRotateObjectRigidbody.AddForce(moveForce * 10000f, ForceMode.Force);
    }

    private void Update()
    {
        character.position = moveRotateObject.position;
        bool isMove = MoveInput(Time.deltaTime);
        CharacterMoveAnimation(isMove);
        CameraRotation();
        CharacterRotation(Time.deltaTime);
    }

    /// <summary>
    /// プレイヤー移動
    /// </summary>
    private bool MoveInput(float deltaTime)
    {
        // WASDでプレイヤーを移動
        Vector3 moveVector = Vector3.zero;
        bool isMove = false;
        
        bool moveForward = Input.GetKey(KeyCode.W);
        bool moveBack = Input.GetKey(KeyCode.S);
        bool moveLeft = Input.GetKey(KeyCode.A);
        bool moveRight = Input.GetKey(KeyCode.D);
        if (!(moveForward && moveBack))
        {
            if (moveForward)
            {
                moveVector += moveRotateObject.forward;
                isMove = true;
            }
            else if (moveBack)
            {
                moveVector -= moveRotateObject.forward;
                isMove = true;
            }
        }
        if (!(moveLeft && moveRight))
        {
            if (moveLeft)
            {
                moveVector -= moveRotateObject.right;
                isMove = true;
            }
            else if (moveRight)
            {
                moveVector += moveRotateObject.right;
                isMove = true;
            }
        }
        
        if (!isMove)
        {
            moveForce = Vector3.zero;
        }
        else
        {
            moveVector = Vector3.Normalize(moveVector);
            moveForce = moveVector * playerMoveForce * deltaTime;
            characterFaceDirection = moveVector;
        }

        return isMove;
    }

    /// <summary>
    /// 画面ドラックで、カメラを回転
    /// </summary>
    private void CameraRotation()
    {
        if (!Input.GetMouseButton(0))
        {
            mouseLastPosition = Input.mousePosition;
            return;
        }

        Vector3 mouseDeltaPosition = Input.mousePosition - mouseLastPosition;
        mouseLastPosition = Input.mousePosition;
        
        // 水平回転
        Quaternion horizonRotation = Quaternion.AngleAxis(-mouseDeltaPosition.x * rotateSpeedHorizon, Vector3.up);
        moveRotateObject.rotation = horizonRotation * moveRotateObject.rotation;
        
        // 垂直回転
        Vector3 verticalEulerAngles = verticalRotationObject.eulerAngles;
        if (verticalEulerAngles.x > 180)
        {
            verticalEulerAngles.x -= 360;
        }
        verticalEulerAngles.x -= mouseDeltaPosition.y * rotateSpeedVertical;
        verticalEulerAngles.x = Mathf.Clamp(verticalEulerAngles.x, rotateVerticalMin, rotateVerticalMax);
        verticalRotationObject.eulerAngles = verticalEulerAngles;
    }

    /// <summary>
    /// キャラクター回転
    /// </summary>
    private void CharacterRotation(float deltaTime)
    {
        float rotInterval = deltaTime * characterRotateInterval;
        Vector3 faceDirection = Vector3.Slerp(character.forward, characterFaceDirection, rotInterval);
        Quaternion rotation = Quaternion.LookRotation(Vector3.Normalize(faceDirection));
        character.rotation = rotation;
    }

    private void CharacterMoveAnimation(bool isMove)
    {
        bool isRunning = characterAnimator.GetBool("Run");
        if (isRunning)
        {
            if (!isMove)
            {
                characterAnimator.SetBool("Run", false);
            }
        }
        else
        {
            if (isMove)
            {
                characterAnimator.SetBool("Run", true);
            }
        }
    }
}
