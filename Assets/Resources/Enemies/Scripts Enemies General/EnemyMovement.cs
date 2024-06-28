using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed;

    private EnemyInfoSO enemyInfo;
    private Transform player;

    private void Start()
    {
        player = PlayerInfo.instance.player.transform;

        enemyInfo = GetComponent<SOFinderEnemy>().enemyInfoSO;

        currentSpeed = enemyInfo.speed;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!NearPlayer())
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * currentSpeed * Time.deltaTime);
        }
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
