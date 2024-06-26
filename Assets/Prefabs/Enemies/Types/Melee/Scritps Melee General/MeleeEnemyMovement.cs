using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    [Header("Enemy Scriptable Object")]
    [Tooltip("Scriptable Object of the enemy.")]
    [SerializeField] EnemyInfoSO enemyInfo;

    [HideInInspector]
    public float currentSpeed;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentSpeed = enemyInfo.speed;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > enemyInfo.attackRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * currentSpeed * Time.deltaTime);
        }
    }
}
