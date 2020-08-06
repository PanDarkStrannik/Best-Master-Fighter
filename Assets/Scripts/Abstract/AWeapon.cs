using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour, IWeapon
{

    [SerializeField] protected List<DamageByType> weaponData;
    [SerializeField] protected WeaponType weaponType;
    [SerializeField] protected GameObject weaponObject;

    public WeaponType WeaponType
    {
        get
        {
            return weaponType;
        }
    }

    public GameObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
    }


    public abstract void Attack();
     
}

public enum WeaponType
{
    Mili, Range
}