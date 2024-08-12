using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomStatsUpgradeManager : MonoBehaviour
{
    public static event Action EventTriggerOnUpgradeStat;

    [SerializeField] Button[] upgradeCards;

    private PlayerUpgradeEnum[] keyButtons;

    Dictionary<PlayerUpgradeEnum, int> dictionary;


    private void OnEnable()
    {
        dictionary = PlayerStats.instance.GetComponent<SOFinderPlayer>().sOPlayerInfo.statUpgrades;

        keyButtons = new PlayerUpgradeEnum[upgradeCards.Length];

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            int index = i;

            AssingRandomStat(upgradeCards[index], out keyButtons[index]);

            upgradeCards[index].onClick.AddListener(() => IncrementValue(keyButtons[index]));
        }
    }

    void AssingRandomStat(Button button, out PlayerUpgradeEnum key)
    {
        int randomIndex = UnityEngine.Random.Range(0, dictionary.Count);
        key = dictionary.ElementAt(randomIndex).Key;

        button.GetComponentInChildren<TextMeshProUGUI>().text = key.ToString();
    }

    void IncrementValue(PlayerUpgradeEnum key)
    {
        foreach (PlayerUpgradeEnum stat in System.Enum.GetValues(typeof(PlayerUpgradeEnum)))
        {
            if (stat == key)
            {
                dictionary[stat]++;
            }
        }

        Time.timeScale = 1;

        EventTriggerOnUpgradeStat?.Invoke();

        gameObject.SetActive(false);

    }

}
