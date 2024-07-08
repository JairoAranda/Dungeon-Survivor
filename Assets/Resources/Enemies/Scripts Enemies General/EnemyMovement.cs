using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
[RequireComponent(typeof(DistanceToPlayer))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed;

    private SOEnemyInfo enemyInfo;
    private Transform player;
    private DistanceToPlayer distanceToPlayer;

    private void Start()
    {
        player = PlayerStats.instance.player.transform;

        distanceToPlayer = GetComponent<DistanceToPlayer>();

        enemyInfo = GetComponent<SOFinderEnemy>().enemyInfoSO;

        currentSpeed = enemyInfo.speed;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!distanceToPlayer.NearPlayer())
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * currentSpeed * Time.deltaTime);
        }
    }

}
