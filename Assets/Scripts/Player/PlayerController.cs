﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(APlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);


    private APlayerMovement movement;
    private PlayerInput input;
    private IWeapon weapons;
    private OldHP hp;

    private bool isShiftInput=false;

    private float moveX, moveY;
   




    private void Awake()
    {
        weapons = GetComponent<IWeapon>();
        hp = GetComponent<OldHP>();
        input = new PlayerInput();
        movement = GetComponent<APlayerMovement>();
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }


    void Update()
    {
        MovementInput();
        RotationInput();
    }

    private void MovementInput()
    {

        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
        var correctMove = new Vector3(moveDirection.x, 0, moveDirection.y);
        correctMove = transform.TransformDirection(correctMove);

        movement.Move(correctMove);

    }

    private void RotationInput()
    {
        var rotationInput = input.RotationInput.GetRotation.ReadValue<Vector2>();

        moveY -= rotationInput.y * SensY;
        moveY = ClampAngle(moveY, MinMax_Y.x, MinMax_Y.y);

        moveX += rotationInput.x * SensX;

        transform.rotation = Quaternion.Euler(0, moveX, 0);

        if (cameraOnPlayer != null)
        {
            cameraOnPlayer.rotation = Quaternion.Euler(moveY, moveX, 0);
        }

    }


    private void ButtonsInput()
    {
        input.ButtonInputs.Shoot.performed += context =>
        {
            GameEvents.PlayerAction(GameEvents.PlayerEvents.Shoot);
            weapons.Attack();
        };


        //input.ButtonInputs.Reload.performed += context => StartCoroutine(weapons.Reload());
        input.ButtonInputs.ChangeSpeed.performed += context =>
        {
            if (!isShiftInput)
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Fast;
                isShiftInput = true;
            }
            else
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Slow;
                isShiftInput = false;
            }
        };
        input.ButtonInputs.Heal.performed += context => hp.heal = true;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

}

