using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    private Transform player;

    private SOEnemyInfo enemyInfo;

    private void Start()
    {
        player = PlayerStats.instance.transform;

        enemyInfo = GetComponent<SOFinderEnemy>().enemyInfoSO;
    }

    public bool NearPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > enemyInfo.attackRange)
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
