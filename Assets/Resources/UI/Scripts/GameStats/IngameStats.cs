using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameStats : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeEnum stat;

    TextMeshProUGUI statText;

    SOPlayerInfo playerInfo;

    private void OnEnable()
    {
        playerInfo = PlayerStats.instance.soPlayerInfo;

        statText = GetComponentInChildren<TextMeshProUGUI>();

        string name = StringUtils.CapitalizeFirstLetter(stat.ToString());

        statText.text = name + " : " + playerInfo.statUpgrades[stat].ToString();
    }

}
