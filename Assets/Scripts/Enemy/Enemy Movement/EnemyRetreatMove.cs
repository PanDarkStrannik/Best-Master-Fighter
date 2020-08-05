using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[System.Serializable]
public class EnemyRetreatMove : AEnemyMovement
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private bool warp = false;
    

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
    }

    public override void Move(Vector3 playerPosition)
    {
        navAgent.ResetPath();
        Vector3 retreatVector = (transform.position - playerPosition).normalized * distance;
        Vector3 newPosition = transform.position + retreatVector;

        if (warp)
        {
            navAgent.Warp(newPosition);
        }
        else
        {
            navAgent.destination = newPosition;
        }
     
    }

}
