using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenStatsManager : MonoBehaviour
{
    [HideInInspector]
    public int enemyKilled; // Número de enemigos eliminados durante el juego.

    [HideInInspector]
    public int levelUp; // Número de veces que el jugador ha subido de nivel durante el juego.

    [HideInInspector]
    public int startLevel; // Nivel de inicio del jugador al comenzar el juego.

    private void OnEnable()
    {
        EnemyStats.EventTriggerDeathEnemy += EnemyKill;
        PlayerStats.EventTriggerLevelUp += LevelUp;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerDeathEnemy -= EnemyKill;
        PlayerStats.EventTriggerLevelUp -= LevelUp;
    }

    private void Start()
    {
        // Establece el nivel de inicio del jugador
        startLevel = PlayerStats.instance.lvl;
    }

    // Incrementa el conteo de enemigos eliminados.
    void EnemyKill(GameObject go)
    {
        enemyKilled++;
    }

    // Incrementa el conteo de niveles subidos por el jugador.
    void LevelUp()
    {
        levelUp++;
    }
}
