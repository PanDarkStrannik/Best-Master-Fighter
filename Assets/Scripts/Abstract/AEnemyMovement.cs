using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class AEnemyMovement : MonoBehaviour, IMovement
{

    [SerializeField] protected float speed = 10f;

    public EnemyMoveType moveType;

    public abstract void Move(Vector3 direction);
        

    protected NavMeshAgent navAgent;

    public enum EnemyMoveType
    {
        Chase, Retreat, StayOnDistance, WarpChase, WarpRetreat, WarpStayOnDistance
    }    
}


