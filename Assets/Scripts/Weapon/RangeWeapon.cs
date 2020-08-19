using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : AWeapon
{
    [SerializeField] private Spawner bulletSpawner;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private float attackTime;
    [SerializeField] private float toShootTime=0f;

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
            isAttack = true;
            isShoot = true;
            StartCoroutine(Shoot());
        }
    }



    private IEnumerator Shoot()
    {
        events.OnAnimEvent(AnimationController.AnimationType.RangeAttack);
        yield return new WaitForSeconds(toShootTime);
        if (isShoot)
        {
            bulletSpawner.SpawnObject(gunPosition.position, gunPosition.rotation);
        }
        yield return new WaitForSeconds(attackTime);
        isShoot = false;
        isAttack = false;
    }

}
