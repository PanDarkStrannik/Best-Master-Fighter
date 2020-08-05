using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float HP = 50;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
