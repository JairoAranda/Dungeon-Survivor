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
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpdateLate;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= UpdateLifeBar;
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpdateLate;
    }

    void Start()
    {
        UpdateLate();
    }

    private void UpdateLate()
    {
        StartCoroutine(LateCall());
    }

    void UpgradeMaxLife()
    {
        maxHealth = PlayerStats.instance.maxLife;

        UpdateLifeBar(gameObject);
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(0.1f);

        UpgradeMaxLife();
    }

    void UpdateLifeBar(GameObject go)
    {
        healthBar.fillAmount = PlayerStats.instance.life / maxHealth;
    }
}
