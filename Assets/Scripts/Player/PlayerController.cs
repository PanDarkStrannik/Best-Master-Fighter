using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(APlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraOnPlayer;

    [SerializeField] private float SensX = 5, SensY = 10;
    [SerializeField] private Vector2 MinMax_Y = new Vector2(-40, 40);

    [SerializeField] private WeaponChanger weaponChanger;

    [SerializeField] protected DamagebleParamSum paramSum;

    [SerializeReference] private List<GameObject> deactiveOnDeath;

    private APlayerMovement movement;
    private PlayerInput input;

    private bool isShiftNotInput = true;

    private float moveX, moveY;
   
    



    private void Awake()
    {
        input = new PlayerInput();
        movement = GetComponent<APlayerMovement>();
        weaponChanger.ChangeWeapon(0);
    }

    void Start()
    {
        ButtonsInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UI.playerHealth = paramSum.typesValues[DamagebleParam.ParamType.Health];
    }

    private void OnEnable()
    {
        input.Enable();
        paramSum.OnParamNull += CheckType;
        paramSum.Initialize();
    }
    private void OnDisable()
    {
        input.Disable();
        paramSum.OnParamNull -= CheckType;
        paramSum.Unsubscribe();
    }


    void Update()
    {
        UI.playerHealth = paramSum.typesValues[DamagebleParam.ParamType.Health];
        RotationInput();
    }


    private void CheckType(DamagebleParam.ParamType type)
    {
        switch (type)
        {
            case DamagebleParam.ParamType.Health:      
                foreach (var obj in deactiveOnDeath)
                {
                    obj.SetActive(false);
                }
                UI.playerHealth = 0;
                Debug.Log("Игрока убили");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
        }
    }


    private void FixedUpdate()
    {
        var moveDirection = input.MovementInput.GetDirection.ReadValue<Vector2>();
        var correctMove = new Vector3(moveDirection.x, input.ButtonInputs.Jump.ReadValue<float>(), moveDirection.y);
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

            weaponChanger.CurrentWeapon.Attack();
        };


        input.ButtonInputs.ChangeWeapon.performed += context => weaponChanger.ChangeWeapon(input.ButtonInputs.ChangeWeapon.ReadValue<float>());


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


    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle -= 360F;
        if (angle > 360F) angle += 360F;
        return Mathf.Clamp(angle, min, max);
    }

}

