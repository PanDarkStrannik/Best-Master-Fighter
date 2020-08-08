using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliWeapon : AWeapon
{
    [SerializeField] private Collider miliWeapon;
    [SerializeField] private Animator weaponAnim;
    [SerializeField] private float timeWeaponColider=1f;

    private bool weaponActive = false;

    //public override void Attack()
    //{
    //    Ray attackRay = new Ray(target.position, target.forward);
    //    if (Physics.SphereCast(attackRay, area, out RaycastHit hit, range))
    //    {
    //        Debug.Log("Что-то увидели: " + hit.transform.name);
    //        if (hit.transform.GetComponent<IDamageble>() != null)
    //        {
    //            var tmp = hit.transform.GetComponent<IDamageble>();
    //            foreach (var weapon in weaponData)
    //            {
    //                tmp.ApplyDamage(weapon);
    //            }
    //        }
    //    }

    //}

    private void Start()
    {
        miliWeapon.enabled = false;
        miliWeapon.isTrigger = true;
    }

    public override void Attack()
    {
        if (!weaponActive)
        {
            weaponAnim.SetTrigger("Attack");
            StartCoroutine(ChangeColider());    
        }
    }

    private IEnumerator ChangeColider()
    {
        weaponActive = true;
        miliWeapon.enabled = weaponActive;
        yield return new WaitForSeconds(timeWeaponColider);
        weaponActive = false;
        miliWeapon.enabled = weaponActive;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(weaponActive)
        {
            if(other.transform.GetComponent<IDamageble>()!=null)
            {
                Debug.Log("Что-то задели!");
                foreach (var data in weaponData)
                {
                    other.transform.GetComponent<IDamageble>().ApplyDamage(data);
                }
            }
        }
    }


   
}
