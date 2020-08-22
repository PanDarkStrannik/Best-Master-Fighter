using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : AWeapon
{
    [SerializeField] private float toBlinkTime=0.2f;
    [SerializeField] private float afterBlink = 5f;
    [SerializeField] private Transform blinkBody;
    [SerializeField] private float blinkDistance=3f;
    [SerializeField] private Color gizmosColor;
    [SerializeField] private Transform blinkGun;
    
    public float ReloadTime
    {
        get
        {
            return afterBlink;
        }
    }

    public override void Attack()
    {
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(toBlinkTime);
        Teleport();
        yield return new WaitForSeconds(afterBlink);
        isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(blinkGun.position, 1f);
        Gizmos.DrawSphere(blinkGun.position + blinkGun.forward * blinkDistance, 1f);
    }

    private void Teleport()
    {
        blinkBody.transform.position = blinkGun.position + blinkGun.forward * blinkDistance;
    }

    private void DamageOtherObjects()
    {

    }
}
