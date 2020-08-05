using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AEnemyMovement))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private List<float> detectionDistances;
    [SerializeField] private List<Color> colors;
    [SerializeField] private List<AEnemyMovement.EnemyMoveType> enemyMoveTypes;

    private List<AEnemyMovement> movements;

    public bool IsDetectingPlayer = true;


    private Dictionary<float, Color> detectionColor;
    private Dictionary<float, AEnemyMovement.EnemyMoveType> detectionMoveTypes;

    private Transform playerTransform;

    private void Start()
    {

        movements = new List<AEnemyMovement>(GetComponents<AEnemyMovement>());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        detectionMoveTypes = new Dictionary<float, AEnemyMovement.EnemyMoveType>();
        for (int i = 0; i < detectionDistances.Count; i++)
        {
            detectionMoveTypes.Add(detectionDistances[i], enemyMoveTypes[i]);
        }


    }


    private void Update()
    {
        if(IsDetectingPlayer)
        {
            DetectionPlayer();
        }

    }

    private void DetectionPlayer()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;

        foreach (var element in detectionMoveTypes)
        {
            if (toPlayer.magnitude < element.Key)
            {
                Move(element.Value);
                break;
            }
        }
    }


    public void Move(AEnemyMovement.EnemyMoveType currentType, Transform target)
    {
        foreach (var movement in movements)
        {
            if (movement.moveType == currentType)
            {
                transform.LookAt(target);
                movement.Move(target.position);
            }
        }
    }

    public void Move(AEnemyMovement.EnemyMoveType currentType)
    {
        Move(currentType, playerTransform);
    }



    private void OnDrawGizmos()
    {

        detectionColor = new Dictionary<float, Color>();
        for (int i = 0; i < detectionDistances.Count; i++)
        {
            detectionColor.Add(detectionDistances[i], colors[i]);
        }


        foreach (var element in detectionColor)
        {
            Gizmos.color = element.Value;
            Gizmos.DrawSphere(transform.position, element.Key);
        }
    }

}
