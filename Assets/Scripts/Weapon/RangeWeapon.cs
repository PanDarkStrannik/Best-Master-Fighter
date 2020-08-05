using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] private Spawner bulletSpawner;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float attackTime;

    public bool shoot; // это временная переменная, просто чтобы потестить

    private void Awake()
    {
        bulletSpawner.CreateSpawner();
        GameEvents.onBulletDie+=bulletSpawner.ReturnObject;
        foreach(var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<SimpleBullet>().Init(weaponData[0]);
        }
    }


    private void Update()
    {
        if(shoot)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        bulletSpawner.SpawnObject(gunPosition.position, gunPosition.rotation);
        StartCoroutine(DelayBetweenAttack());
    }

    private IEnumerator DelayBetweenAttack()
    {
        yield return new WaitForSeconds(attackTime);
    }
}
