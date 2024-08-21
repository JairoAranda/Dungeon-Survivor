using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmount : MonoBehaviour
{    
    private int luck;

    [SerializeField] private PlayerUpgradeEnum luckUpgrade;

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += GetLuck;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= GetLuck;
    }

    void Start()
    {
        GetLuck();
    }

    void GetLuck()
    {
        luck = PlayerStats.instance.GetComponentInParent<SOFinderPlayer>().sOPlayerInfo.statUpgrades[luckUpgrade];
    }

    public int GetDropNumber(int minDrop, int maxDrop, double probabilityMaxDrop)
    {
        probabilityMaxDrop += PlayerPrefs.GetInt("Luck", 1) * 0.1f - 0.1f;
        probabilityMaxDrop = Mathf.Clamp((float)probabilityMaxDrop, 0.01f, 1.0f);

        luck = Mathf.Clamp(luck, 1, 20);

        int range = maxDrop - minDrop;

        float luckFactor = Mathf.Clamp01(Mathf.Pow((float)luck / 20f, 2) * (float)probabilityMaxDrop);

        float randomValue = Random.Range(0f, 1f);

        int result;
        if (randomValue <= luckFactor)
        {
            result = maxDrop;
        }

        else
        {
            result = Mathf.FloorToInt(minDrop + randomValue * range);
        }

        result = Mathf.Clamp(result, minDrop, maxDrop);

        return result;
    }
}
