using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameStats : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeEnum stat;

    TextMeshProUGUI statText;

    SOPlayerInfo playerInfo;

    private void Start()
    {
        playerInfo = PlayerStats.instance.soPlayerInfo;

        statText = GetComponentInChildren<TextMeshProUGUI>();

        statText.text = stat.ToString() + " : " + playerInfo.statUpgrades[stat].ToString();
    }
}
