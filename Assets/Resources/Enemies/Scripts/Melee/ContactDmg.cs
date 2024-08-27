using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactDmg : MonoBehaviour
{
    private SOEnemyInfo enemyInfoSO;

    void Start()
    {
        enemyInfoSO = GetComponentInParent<SOFinderEnemy>().enemyInfoSO;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats.instance.GetHit(enemyInfoSO.damage);
        }
    }

}
