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

    private PlayerUpgradeEnum?[] keyButtons;

    private HashSet<PlayerUpgradeEnum> assignedValues;

    Dictionary<PlayerUpgradeEnum, int> dictionary;


    private void OnEnable()
    {
        dictionary = PlayerStats.instance.soPlayerInfo.statUpgrades;

        keyButtons = new PlayerUpgradeEnum?[upgradeCards.Length];
        assignedValues = new HashSet<PlayerUpgradeEnum>();

        foreach (var upgradeCard in upgradeCards)
        {
            upgradeCard.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < upgradeCards.Length; i++)
        {
            int index = i;

            AssingRandomStat(upgradeCards[index], out keyButtons[index]);

            if (keyButtons[index].HasValue)
            {
                upgradeCards[index].onClick.AddListener(() => IncrementValue(keyButtons[index].Value));
            }
            else
            {
                upgradeCards[index].onClick.AddListener(() => GetMoney());
            }
        }
    }

    void AssingRandomStat(Button button, out PlayerUpgradeEnum? key)
    {
        var availableKeys = dictionary.Where(kv => kv.Value < 20 && !assignedValues.Contains(kv.Key))
                                    .Select(kv => kv.Key)
                                    .ToList();

        if (availableKeys.Count == 0)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Dinero";
            key = null;
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableKeys.Count);

        key = availableKeys[randomIndex];

        assignedValues.Add(key.Value);

        string name = StringUtils.CapitalizeFirstLetter(key.ToString());

        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
    }


    void IncrementValue(PlayerUpgradeEnum key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key]++;

            Debug.Log(dictionary[key]);
        }

        EventTriggerOnUpgradeStat?.Invoke();

        gameObject.SetActive(false);
    }

    void GetMoney()
    {
        MoneyManager.instance.money += 30;

        gameObject.SetActive(false);
    }

}
