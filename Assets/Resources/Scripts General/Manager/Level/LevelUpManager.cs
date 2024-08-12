using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] GameObject upgradeOptionGO;

    private void OnEnable()
    {
        PlayerStats.EventTriggerLevelUp += EnableLevelUpOptions;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerLevelUp -= EnableLevelUpOptions;
    }

    void EnableLevelUpOptions()
    {
        Time.timeScale = 0f;

        upgradeOptionGO.SetActive(true);
    }
}
