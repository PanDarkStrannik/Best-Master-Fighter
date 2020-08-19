using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private List<DamageByType> damages;
    [SerializeField] private Collider damageCollider;
    [SerializeField] private Vector3 colliderScale;
    [SerializeField] private Color gizmosColor = Color.red;
    [SerializeField] private float timeBetweenDamage = 1f;

    [SerializeField] private Transform parent;

    private Dictionary<Collider,bool> enterColiders;

    public Transform Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    private void Start()
    {
        enterColiders = new Dictionary<Collider, bool>();

        if(parent!=null)
        {
            damageCollider.transform.parent = parent;
        }
        damageCollider.isTrigger = true;
    }


    public void ChangeArea(Vector3 scale)
    {
        damageCollider.transform.localScale = colliderScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!enterColiders.ContainsKey(other))
        {
            enterColiders.Add(other, false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageble>() != null)
        {
            if (!enterColiders[other])
            {
                StartCoroutine(GetDamage(other));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enterColiders.ContainsKey(other))
        {
            enterColiders.Remove(other);
        }
    }

    private IEnumerator GetDamage(Collider other)
    {
        enterColiders[other] = true;
        foreach (var damage in damages)
        {
           other.GetComponent<IDamageble>().ApplyDamage(damage);
        }
        yield return new WaitForSeconds(timeBetweenDamage);
        enterColiders[other] = false;
    }

    private void OnDrawGizmos()
    {
        ChangeArea(colliderScale);
        Gizmos.color = gizmosColor;
        Gizmos.DrawCube(damageCollider.transform.position, colliderScale);
    }


}
