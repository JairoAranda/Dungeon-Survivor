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
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += Upgrade;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= UpdateLifeBar;
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= Upgrade;
    }

    void Start()
    {
        StartCoroutine(LateStart());
    }

    void Upgrade()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();
        UpgradeMaxLife();
    }

    void UpgradeMaxLife()
    {
        maxHealth = PlayerStats.instance.maxLife;

        UpdateLifeBar(gameObject);
    }


    void UpdateLifeBar(GameObject go)
    {
        if (PlayerStats.instance != null)
        {
            healthBar.fillAmount = PlayerStats.instance.life / maxHealth;
        }
    }
}
