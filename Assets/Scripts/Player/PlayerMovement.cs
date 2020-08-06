using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : APlayerMovement
{
    [SerializeField] private List<PlayerMoveType> moveTypes;
    [SerializeField] private List<float> speeds;
    [SerializeField] private float gravity=9.8f;

    private CharacterController controller;
    private Dictionary<PlayerMoveType, float> moveTypeSpeeds;
    //private Rigidbody body;

    private void Start()
    {
        moveTypeSpeeds = new Dictionary<PlayerMoveType, float>();
        controller = GetComponent<CharacterController>();
        //body = GetComponent<Rigidbody>();

        for (int i = 0; i < moveTypes.Count; i++)
        {
            moveTypeSpeeds.Add(moveTypes[i], speeds[i]);
        }
    }

    public override void Move(Vector3 direction)
    {
        foreach(var type in moveTypeSpeeds)
        {
            if (moveType == type.Key)
            {
                if(!controller.isGrounded)
                {
                    controller.Move(-transform.up * Time.deltaTime * gravity);
                }
                else
                {
                    controller.Move(direction.normalized * Time.deltaTime * type.Value);
                }
                //body.AddForce(direction.normalized * type.Value);
            }
        }
    }

}
