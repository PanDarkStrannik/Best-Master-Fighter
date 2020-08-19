using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{

    [HideInInspector] public bool UponDistance = true;
    [HideInInspector] public Transform target = null;
    [SerializeReference] private List<AEnemyMovement> movements;
    [SerializeReference] private List<Transform> lookingObject;
    [SerializeReference] private NavMeshAgent meshAgent;
    [SerializeReference] private MainEvents mainEvents;
    [SerializeField] private float correctSpeedToAnim=10;

    public void Move(AEnemyMovement.EnemyMoveType currentType, Transform target)
    {
        foreach (var movement in movements)
        {
            if (movement.moveType == currentType)
            {
                foreach(var lookObj in lookingObject)
                {
                    lookObj.LookAt(target);
                } 
                movement.Move(target.position);
            }
        }
    }

    public void Move(AEnemyMovement.EnemyMoveType currentType)
    {
        Move(currentType, target);
    }


    public bool MoveUponDistance(Transform target, float detectionDistance, AEnemyMovement.EnemyMoveType currentType)
    {
        Vector3 toTarget = target.position - transform.position;
        if (toTarget.magnitude <= detectionDistance)
        {
            foreach (var lookObj in lookingObject)
            {
                lookObj.LookAt(target);
            }
            Move(currentType, target);
            mainEvents.OnAnimEvent(AnimationController.AnimationType.Movement, meshAgent.velocity.magnitude / correctSpeedToAnim);
            return true;
        }
        return false;
    }


}
