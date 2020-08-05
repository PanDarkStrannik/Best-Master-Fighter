using UnityEngine.UI;
using UnityEngine;
using System;

public class HPtext : MonoBehaviour
{
    public Text hpText;
    public Text kitsText;
    public Text ammo;
    public Text currentAmmo;

    //cooldowns
    public Image imageCooldown;
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

        hpText.text = OldHP.health.ToString();
        kitsText.text = OldHP.kitNumber.ToString();
        ammo.text = "\u221E";
        currentAmmo.text = Weapons.currentAmmo.ToString();
    }
}
