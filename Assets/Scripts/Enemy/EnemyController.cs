using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<GameEvents.PlayerEvents> onPlayerEvents;
    [SerializeField] private List<AEnemyMovement.EnemyMoveType> moveOnPlayerEvents;

    [SerializeField] private List<GameEvents.EnemyEvents> onEnemyEvents;
    [SerializeField] private List<AEnemyMovement.EnemyMoveType> moveOnEnemyEvents;

    private Dictionary<GameEvents.PlayerEvents, AEnemyMovement.EnemyMoveType> onPlayerEventMove;
    private Dictionary<GameEvents.EnemyEvents, AEnemyMovement.EnemyMoveType> onEnemyEventMove;

    private EnemyMovementController movement;

    public void Start()
    {
        onPlayerEventMove = new Dictionary<GameEvents.PlayerEvents, AEnemyMovement.EnemyMoveType>();
        onEnemyEventMove = new Dictionary<GameEvents.EnemyEvents, AEnemyMovement.EnemyMoveType>();

        for(int i=0; i<onPlayerEvents.Count;i++)
        {
            onPlayerEventMove.Add(onPlayerEvents[i], moveOnPlayerEvents[i]);
        }
        for (int i = 0; i < onEnemyEvents.Count; i++)
        {
            onEnemyEventMove.Add(onEnemyEvents[i], moveOnEnemyEvents[i]);
        }

        movement = GetComponent<EnemyMovementController>();

        GameEvents.PlayerAction += OnPlayerEvent;
        GameEvents.EnemyAction += OnEnemyEvent;
    }

    private void OnPlayerEvent(GameEvents.PlayerEvents playerEvent)
    {
        foreach(var element in onPlayerEvents)
        {
            if(playerEvent==element)
            {
                movement.IsDetectingPlayer = false;
                movement.Move(onPlayerEventMove[playerEvent]);
                break;
            }
        }

        StartCoroutine(ActivateDetectionBeforeTime(3));
    }

    private void OnEnemyEvent(GameEvents.EnemyEvents enemyEvent)
    {
        foreach (var element in onEnemyEvents)
        {
            if (enemyEvent == element)
            {
                movement.IsDetectingPlayer = false;
                movement.Move(onEnemyEventMove[enemyEvent]);
                break;
            }
        }

        StartCoroutine(ActivateDetectionBeforeTime(3));
    }


    private IEnumerator ActivateDetectionBeforeTime(float time)
    {
        yield return new WaitForSeconds(time);
        movement.IsDetectingPlayer = true;
    }
}


