using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Image healthBar; // Imagen de la barra de vida

    float maxHealth; // Valor máximo de salud del jugador

    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += UpdateLifeBar;
        PlayerStats.EventTriggerHealthRegen += UpdateLifeBar;
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += Upgrade;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= UpdateLifeBar;
        PlayerStats.EventTriggerHealthRegen -= UpdateLifeBar;
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
        // Actualiza el valor máximo de salud y luego actualiza la barra de vida
        maxHealth = PlayerStats.instance.maxLife;
        UpdateLifeBar(gameObject);
    }


    void UpdateLifeBar(GameObject go)
    {
        // Actualiza la barra de vida en función de la salud actual del jugador
        if (PlayerStats.instance != null)
        {
            healthBar.fillAmount = PlayerStats.instance.life / maxHealth;
        }
    }
}
