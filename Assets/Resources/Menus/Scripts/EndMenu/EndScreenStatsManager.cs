using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenStatsManager : MonoBehaviour
{

    [HideInInspector]
    public int enemyKilled;

    [HideInInspector]
    public int levelUp;

    [HideInInspector]
    public int startLevel;

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
        startLevel = PlayerStats.instance.lvl;
    }

    void EnemyKill(GameObject go)
    {
        enemyKilled++;
    }

    void LevelUp()
    {
        levelUp++;
    }
}
