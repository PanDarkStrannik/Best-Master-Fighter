using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public static float currentAmmo;
    [SerializeField] private float ammoAtOnce;
    public static bool reloading;
    [SerializeField] public static float ammo;
    [SerializeField] private float reloadTime;
    private float currentReloadingTime;
    [SerializeField] public int damage;
    [SerializeField] private float shootingSpeed;
    [SerializeField] private float range;
    [SerializeField] private float pushForce;
    [SerializeField] ParticleSystem ps;
    [SerializeField] private GameObject popup;

    public static bool shooting;

    [SerializeField] Camera camera;

    private void Awake()
    {
        ammoAtOnce = 5;
        shooting = false;
        currentAmmo = ammoAtOnce;
        reloading = false;
        ammo = 90;
        reloadTime = 1.3f;
        currentReloadingTime = reloadTime;
        
    }

    private void Update()
    {
     

        if (currentAmmo == 0)
        {
            reloading = false;
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        if (!reloading && !shooting && !OldHP.healing && ammo > 0)
        {
            reloading = true;
            yield return new WaitForSeconds(reloadTime);
            if (ammo + currentAmmo > ammoAtOnce)
            {
                ammo -= ammoAtOnce - currentAmmo;
                currentAmmo = ammoAtOnce;
            }

            else
            {
                currentAmmo += ammo;
                ammo = 0;
            }
            reloading = false;
        }        
    }

    /*private void Reload()
    {
        if (currentReloadingTime > 0) currentReloadingTime -= Time.deltaTime;
        else
        {


            if (ammo + currentAmmo > ammoAtOnce)
            {
                ammo -= ammoAtOnce - currentAmmo;
                currentAmmo = ammoAtOnce;
            }

            else
            {
                currentAmmo += ammo;
                ammo = 0;
            }

            reloading = false;
            currentReloadingTime = reloadTime;
        }
    }
    */
    public void Shoot()
    {
        if (currentAmmo > 0 && !reloading)
        {
            currentAmmo -= 1;
            shooting = true;
            ps.Play();

            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
            {
                Debug.Log("Something Hitted");
                EnemyHP target = hit.transform.GetComponent<EnemyHP>();



                if (target != null)
                {
                    var tmp=Instantiate(popup, hit.point, transform.rotation);
                    tmp.transform.LookAt(transform.position);
                    Destroy(tmp, 1f);

                    target.TakeDamage(damage);
                }
            }

            shooting = false;

        }
    }
}
