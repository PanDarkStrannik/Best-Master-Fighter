using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour, IBullet
{
    [SerializeField] private float toDieTime = 5f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<DamageByType> bulletDatas;

    private bool notFistInit = false;

    public void Init(List<DamageByType> datas)
    {
        List<DamageByType> tmp = new List<DamageByType>();
        List<DamageByType> mainWeaponDatas = new List<DamageByType>(datas);
        List<DamageByType> bulletDatas = new List<DamageByType>(this.bulletDatas);

        foreach (var mainWeapon in datas)
        {
            foreach (var myData in this.bulletDatas)
            {
                if (mainWeapon.DamageType == myData.DamageType)
                {
                    tmp.Add(new DamageByType(mainWeapon.DamageType, mainWeapon.Value + myData.Value));
                    bulletDatas.Remove(myData);
                    mainWeaponDatas.Remove(mainWeapon);
                }
            }
        }

        if (mainWeaponDatas.Count > 0)
        {
            tmp.AddRange(mainWeaponDatas);
        }
        if (bulletDatas.Count > 0)
        {
            tmp.AddRange(bulletDatas);
        }

        this.bulletDatas = tmp;

        notFistInit = true;
    }

    private void OnEnable()
    {
        if (notFistInit)
        {
            body.velocity = transform.forward * speed;
            StartCoroutine(ToDie());
        }
    }


    private IEnumerator ToDie()
    {
        yield return new WaitForSeconds(toDieTime);
        GameEvents.onBulletDie(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DamageblePlace>() != null)
        {
            foreach (var data in bulletDatas)
            {
                other.gameObject.GetComponent<IDamageble>().ApplyDamage(data);
            }
            GameEvents.onBulletDie(gameObject);
        }

    }
}
