using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class OldHP : MonoBehaviour
{
    //Игрок
    public static int health = 100;
    public static int armor = 100;




    //Аптечки(сколько хилит, сколько на это тратится времени, количество аптечек)
    [SerializeField] private int giveHealth;
    [SerializeField] private static float timeToUse;
    [SerializeField] public static int kitNumber;
    public static bool healing;

    private float healTimeCD;
 
    [SerializeField] private int passiveHealing =  5;
    [SerializeField] float timeToHeal = 5;

    public bool heal;
    private void Awake()
    {
        kitNumber = 2;
        healing = false;
        giveHealth = 50;
        timeToUse = 1;

    }

    private void Start()
    {
    }

    private void Update()
    {
        //Пассивный отхил
        timeToHeal -= Time.deltaTime;
        if (timeToHeal <= 0) Healing(passiveHealing);


        if (health < 100 && heal && !Weapons.reloading && !healing && kitNumber > 0)
        {
            healing = true;
        }

        //Хилимся
        if (healing) Healing(giveHealth);


    }

    //Как мы получаем урон
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Bullet")) TakeDamage(10);

        //Подобрали аптечку
        //if (other.CompareTag("Kit")) ++kitNumber;
    }


    //Получаем урон или умираем
    public void TakeDamage(int damage)
    {
        timeToHeal = 5;
        if (damage > armor)
        {

            if (health > damage)
            {
                health -= damage - armor;
                armor = 0;
            }
            else
            {
                health = 0;
                Destroy(gameObject);
            }

        }

        else armor -= damage;
    }

    //Хилимся, живем
    public static void Healing(int health)
    {
        if (Weapons.shooting) return;

        if (timeToUse > 0) timeToUse -= Time.deltaTime;

        else
        {
            if (OldHP.health + health <= 100) OldHP.health += health;
            else OldHP.health = 100;

            if(healing) --kitNumber;
            healing = false;
            timeToUse = 1;
            HPtext.healOnCD = true;
        }
    }


}
