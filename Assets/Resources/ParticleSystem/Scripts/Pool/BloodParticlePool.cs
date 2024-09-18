using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BloodParticlePool : GeneralPool
{
    int bloodNumber = -1; // �ndice para rastrear el siguiente objeto de part�culas de sangre a usar

    private void OnEnable()
    {
        // Registra el m�todo SpawnBlood para los eventos de da�o y muerte del jugador y enemigos
        PlayerStats.EventTriggerHitPlayer += SpawnBlood;
        PlayerStats.EventTriggerDeathPlayer += SpawnBlood;
        EnemyStats.EventTriggerHitEnemy += SpawnBlood;
        EnemyStats.EventTriggerDeathEnemy += SpawnBlood;
    }

    private void OnDisable()
    {
        // Desregistra el m�todo SpawnBlood de los eventos para evitar llamadas innecesarias
        PlayerStats.EventTriggerHitPlayer -= SpawnBlood;
        PlayerStats.EventTriggerDeathPlayer -= SpawnBlood;
        EnemyStats.EventTriggerHitEnemy -= SpawnBlood;
        EnemyStats.EventTriggerDeathEnemy -= SpawnBlood;
    }

    void SpawnBlood(GameObject go)
    {
        bloodNumber++;

        // Si el �ndice excede el tama�o del pool, rein�cialo a 0 para reutilizar objetos
        if (bloodNumber > poolSize - 1)
        {
            bloodNumber = 0;
        }

        // Obtiene el siguiente objeto de part�culas de sangre del pool
        GameObject blood = typesInstances[bloodNumber];

        // Establece la posici�n del objeto de part�culas de sangre en la posici�n del objeto afectado
        blood.transform.position = go.transform.position;

        // Reproduce el sistema de part�culas
        blood.GetComponent<ParticleSystem>().Play();
    }
}
