using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
public class EnemyController : MonoBehaviour
{

    [SerializeField] private List<EnemyEventMoveType> enemyMoveEvents;
    [SerializeField] private List<PlayerEventMoveType> playerMoveEvents;

    [SerializeField] private DamagebleParamSum paramSum;

    private EnemyMovementController movement;

    public void Start()
    {
        movement = GetComponent<EnemyMovementController>();

        paramSum.Initialize();

        paramSum.ParamNull += CheckType;

        GameEvents.PlayerAction += OnPlayerEvent;
        GameEvents.EnemyAction += OnEnemyEvent;
    }


    private void OnDisable()
    {
        paramSum.Unsubscribe();
    }



    private void CheckType(DamagebleParam.ParamType type)
    {
        switch(type)
        {
            case DamagebleParam.ParamType.Health:
                gameObject.SetActive(false);
            break;
        }
    }


    private void OnPlayerEvent(GameEvents.PlayerEvents playerEvent)
    {
        if (playerMoveEvents.Count > 0)
        {
            foreach (var element in playerMoveEvents)
            {
                if (playerEvent == element.EventType)
                {
                    movement.IsDetectingPlayer = false;
                    movement.Move(element.MoveType);
                    break;
                }
            }
            StartCoroutine(ActivateDetectionBeforeTime(3));
        }
    }



    private void OnEnemyEvent(GameEvents.EnemyEvents enemyEvent)
    {
        if (playerMoveEvents.Count > 0)
        {
            foreach (var element in enemyMoveEvents)
            {
                if (enemyEvent == element.EventType)
                {
                    movement.IsDetectingPlayer = false;
                    movement.Move(element.MoveType);
                    break;
                }
            }
            StartCoroutine(ActivateDetectionBeforeTime(3));
        }
    }


    private IEnumerator ActivateDetectionBeforeTime(float time)
    {
        yield return new WaitForSeconds(time);
        movement.IsDetectingPlayer = true;
    }
}


[System.Serializable]
public class EnemyEventMoveType
{
    [SerializeField] private AEnemyMovement.EnemyMoveType enemyMoveType;
    [SerializeField] private GameEvents.EnemyEvents enemyEventType;

    public AEnemyMovement.EnemyMoveType MoveType
    {
        get
        {
            return enemyMoveType;
        }
    }

    public GameEvents.EnemyEvents EventType
    {
        get
        {
            return enemyEventType;
        }
    }

}

[System.Serializable]
public class PlayerEventMoveType
{
    [SerializeField] private AEnemyMovement.EnemyMoveType enemyMoveType;
    [SerializeField] private GameEvents.PlayerEvents playerEventType;

    public AEnemyMovement.EnemyMoveType MoveType
    {
        get
        {
            return enemyMoveType;
        }
    }

    public GameEvents.PlayerEvents EventType
    {
        get
        {
            return playerEventType;
        }
    }
}
