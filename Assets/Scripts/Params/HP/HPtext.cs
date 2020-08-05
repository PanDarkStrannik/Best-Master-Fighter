using UnityEngine.UI;
using UnityEngine;
using System;

public class HPtext : MonoBehaviour
{
    public Text hpText;
    public Text kitsText;
    public Text ammo;
    public Text currentAmmo;
    public Text maxHpText;

    //cooldowns
    public Image imageCooldown;
    public Image Hp;
    public static float healCooldown = 30;
    public static bool healOnCD;


    private void Update()
    {

       
        if (healOnCD && healCooldown > 30)
        {
            healCooldown -= Time.deltaTime;
            imageCooldown.fillAmount -= 1 / healCooldown * Time.deltaTime;
        }
        else healOnCD = false;

        Hp.fillAmount = OldHP.health / 100;

        maxHpText.text = OldHP.maxHealth.ToString();
        hpText.text = OldHP.health.ToString();
        currentAmmo.text = Weapons.currentAmmo.ToString();
    }
}
