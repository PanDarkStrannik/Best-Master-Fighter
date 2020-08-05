using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    [SerializeField] private float toDieTime=5f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed=5f;
    private DamageByType weaponData;
    private bool notFistInit = false;
   


    public void Init(DamageByType data)
    {
        weaponData = data;
        notFistInit = true;
    }

    private void OnEnable()
    {
        if(notFistInit)
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
        if (other.gameObject.GetComponent<ADamageble>() != null)
        {
            other.gameObject.GetComponent<IDamageble>().ApplyDamage(weaponData);
           GameEvents.onBulletDie(gameObject);
        }
        
    }
}
