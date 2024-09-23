using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    private Transform player; // Referencia al transform del jugador

    private SOEnemyInfo enemyInfo; // Referencia a la informaci�n del enemigo almacenada en un ScriptableObject

    private void Start()
    {
        // Inicializa la referencia al transform del jugador y la informaci�n del enemigo
        player = PlayerStats.instance.transform;
        enemyInfo = GetComponent<SOFinderEnemy>().enemyInfoSO;
    }

    public bool NearPlayer()
    {
        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Compara la distancia con el rango de ataque del enemigo
        if (distanceToPlayer > enemyInfo.attackRange)
        {
            // Si la distancia es mayor que el rango de ataque, el enemigo no est� cerca del jugador
            return false;
        }
        else
        {
            // De lo contrario, el enemigo est� dentro del rango de ataque
            return true;
        }
    }
}
