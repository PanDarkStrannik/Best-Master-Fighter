using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] private Spawner bulletSpawner;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float attackTime;

    private bool isShoot = false;


    private void Awake()
    {
        bulletSpawner.CreateSpawner();
        GameEvents.onBulletDie+=bulletSpawner.ReturnObject;
        foreach(var e in bulletSpawner.spawned_objects)
        {
            e.GetComponent<IBullet>().Init(weaponData);
        }
    }


    public override void Attack()
    {
        if (!isShoot)
        {
            isShoot = true;
            bulletSpawner.SpawnObject(gunPosition.position, gunPosition.rotation);
            Invoke("EnableShoot", attackTime);
        }
    }

    private void EnableShoot()
    {
        isShoot = false;
    }
}
