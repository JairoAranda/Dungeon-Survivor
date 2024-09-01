using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Image healthBar;

    float maxHealth;

    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += UpdateLifeBar;
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpgradeMaxLife;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= UpdateLifeBar;
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpgradeMaxLife;
    }

    void Start()
    {
        UpgradeMaxLife();
    }


    void UpgradeMaxLife()
    {
        maxHealth = PlayerStats.instance.maxLife;

        UpdateLifeBar(gameObject);
    }


    void UpdateLifeBar(GameObject go)
    {
        healthBar.fillAmount = PlayerStats.instance.life / maxHealth;
    }
}
