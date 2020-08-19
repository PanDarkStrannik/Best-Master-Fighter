﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeaponOld : AWeapon
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float area = 5f;
    [SerializeField] private Transform target;

    public override void Attack()
    {
        Ray attackRay=new Ray(target.position,target.forward);        
        if (Physics.SphereCast(attackRay, area, out RaycastHit hit, range))
        {
            Debug.Log("Что-то увидели: " + hit.transform.name);
            if (hit.transform.GetComponent<IDamageble>() != null)
            {
                var tmp = hit.transform.GetComponent<IDamageble>();
                foreach (var weapon in weaponData)
                {
                    tmp.ApplyDamage(weapon);
                }
            }
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(target.position, area);
        Gizmos.DrawSphere(target.position + target.forward * range, area);
    }
}