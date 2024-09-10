using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI level, kills;

    [SerializeField] EndScreenStatsManager endScreenStatsManager;

    private void Start()
    {
        level.text = "LVL " + endScreenStatsManager.startLevel + " -> LVL " + (endScreenStatsManager.startLevel + endScreenStatsManager.levelUp);

        kills.text = endScreenStatsManager.enemyKilled + " kills";
    }
}
