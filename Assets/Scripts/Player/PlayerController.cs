using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(APlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);

    [SerializeField] private WeaponChanger weaponChanger;


    private APlayerMovement movement;
    private PlayerInput input;
    private OldHP hp;

    private bool isShiftNotInput = true;

    private float moveX, moveY;
   




    private void Awake()
    {
        hp = GetComponent<OldHP>();
        input = new PlayerInput();
        movement = GetComponent<APlayerMovement>();
        weaponChanger.ChangeWeapon(0);
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            weaponChanger.CurrentWeapon.Attack();
        };

        input.ButtonInputs.ChangeSpeed.performed += context =>
        {
            if (isShiftNotInput)
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Fast;
                isShiftNotInput = false;
                  
            }
            else
            {
                movement.moveType = APlayerMovement.PlayerMoveType.Slow;
                isShiftNotInput = true;
                
            }
        };
        input.ButtonInputs.Heal.performed += context => hp.heal = true;

        input.ButtonInputs.ChangeWeapon.performed += context => weaponChanger.ChangeWeapon(input.ButtonInputs.ChangeWeapon.ReadValue<float>());

        input.ButtonInputs.Jump.performed += context => movement.Jump = true;

        input.MovementInput.GetDirection.performed += context =>
         {
             if (isShiftNotInput)
             {
                 weaponChanger.CurrentWeapon.Animator.SetBool("Run", false);
                 weaponChanger.CurrentWeapon.Animator.SetBool("Move", true);
             }
             else
             {
                 weaponChanger.CurrentWeapon.Animator.SetBool("Run", true);
                 weaponChanger.CurrentWeapon.Animator.SetBool("Move", false);
             }
         };

        input.MovementInput.GetDirection.canceled += context =>
        {
            weaponChanger.CurrentWeapon.Animator.SetBool("Run", false);
            weaponChanger.CurrentWeapon.Animator.SetBool("Move", false);
        };


    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

}

