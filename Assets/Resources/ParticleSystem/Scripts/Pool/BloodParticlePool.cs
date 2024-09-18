using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodParticlePool : GeneralPool
{
    int bloodNumber = -1; // Índice para rastrear el siguiente objeto de partículas de sangre a usar

    private void OnEnable()
    {
        // Registra el método SpawnBlood para los eventos de daño y muerte del jugador y enemigos
        PlayerStats.EventTriggerHitPlayer += SpawnBlood;
        PlayerStats.EventTriggerDeathPlayer += SpawnBlood;
        EnemyStats.EventTriggerHitEnemy += SpawnBlood;
        EnemyStats.EventTriggerDeathEnemy += SpawnBlood;
    }

    private void OnDisable()
    {
        // Desregistra el método SpawnBlood de los eventos para evitar llamadas innecesarias
        PlayerStats.EventTriggerHitPlayer -= SpawnBlood;
        PlayerStats.EventTriggerDeathPlayer -= SpawnBlood;
        EnemyStats.EventTriggerHitEnemy -= SpawnBlood;
        EnemyStats.EventTriggerDeathEnemy -= SpawnBlood;
    }

    void SpawnBlood(GameObject go)
    {
        bloodNumber++;

        // Si el índice excede el tamaño del pool, reinícialo a 0 para reutilizar objetos
        if (bloodNumber > poolSize - 1)
        {
            bloodNumber = 0;
        }

        // Obtiene el siguiente objeto de partículas de sangre del pool
        GameObject blood = typesInstances[bloodNumber];

        // Establece la posición del objeto de partículas de sangre en la posición del objeto afectado
        blood.transform.position = go.transform.position;

        // Reproduce el sistema de partículas
        blood.GetComponent<ParticleSystem>().Play();
    }
}
