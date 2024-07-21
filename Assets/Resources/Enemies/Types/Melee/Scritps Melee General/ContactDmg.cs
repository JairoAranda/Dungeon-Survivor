using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class ContactDmg : MonoBehaviour
{
    private SOEnemyInfo enemyInfoSO;

    void Start()
    {
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.instance.GetHit(enemyInfoSO.damage);
        }

    }

}
