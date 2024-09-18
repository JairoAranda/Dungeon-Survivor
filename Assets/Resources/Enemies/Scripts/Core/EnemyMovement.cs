using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SOFinderEnemy))]
[RequireComponent(typeof(DistanceToPlayer))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed; // La velocidad actual del enemigo

    private SOEnemyInfo enemyInfo; // Información del enemigo desde el ScriptableObject
    private Transform player; // Referencia al transform del jugador
    private DistanceToPlayer distanceToPlayer; // Referencia al componente DistanceToPlayer

    private void Start()
    {
        // Inicializa la referencia al transform del jugador
        player = PlayerStats.instance.transform;

        // Obtiene los componentes necesarios
        distanceToPlayer = GetComponent<DistanceToPlayer>();
        enemyInfo = GetComponent<SOFinderEnemy>().enemyInfoSO;

        // Establece la velocidad inicial del enemigo desde la información del ScriptableObject
        currentSpeed = enemyInfo.speed;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        // Verifica si el enemigo no está cerca del jugador
        if (!distanceToPlayer.NearPlayer())
        {
            // Calcula la dirección hacia el jugador y normaliza el vector
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Mueve al enemigo hacia el jugador utilizando la velocidad y el tiempo delta
            transform.Translate(directionToPlayer * currentSpeed * Time.deltaTime);
        }
    }

}
