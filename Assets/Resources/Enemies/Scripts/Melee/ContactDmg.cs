using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDmg : MonoBehaviour
{
    private SOEnemyInfo enemyInfoSO;

    void Start()
    {
        enemyInfoSO = GetComponentInParent<SOFinderEnemy>().enemyInfoSO;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.instance.GetHit(enemyInfoSO.damage);
        }

    }

}
