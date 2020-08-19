using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADamageble : MonoBehaviour, IDamageble
{
    [SerializeField] protected DamagebleParamDatas datas;

    public delegate void OnDamagedHelper();
    public event OnDamagedHelper OnDamaged;

    public DamagebleParamDatas Datas
    {
        get
        {
            
            return datas;
            
        }
    }


    public abstract void ApplyDamage(DamageByType weapon);

    protected void DamageEvent()
    {
        OnDamaged?.Invoke();
    }

}
