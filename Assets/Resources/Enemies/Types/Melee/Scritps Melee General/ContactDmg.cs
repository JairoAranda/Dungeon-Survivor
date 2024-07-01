using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class ContactDmg : MonoBehaviour
{
    private EnemyInfoSO enemyInfoSO;

    void Start()
    {
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.instance.Hit(enemyInfoSO.damage);
        }
    }
}
