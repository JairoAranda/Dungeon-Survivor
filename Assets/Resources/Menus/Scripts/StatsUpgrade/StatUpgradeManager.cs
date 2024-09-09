using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeManager : MonoBehaviour
{
    public static event Action<GameObject> EventTriggerAccept, EventTriggerDeny;

    int upgradeMoney;

    int maxLevels;

    GameObject[] levels;

    [SerializeField] PlayerPrefsEnum currentMoneyPrefs;

    public void LevelArray(GameObject levelGO)
    {
        Transform[] childTransforms = levelGO.GetComponentsInChildren<Transform>();
        levels = new GameObject[childTransforms.Length - 1];

        for (int i = 1; i < childTransforms.Length; i++)
        {
            levels[i - 1] = childTransforms[i].gameObject;
        }

        maxLevels = levels.Length;
    }

    public void BaseMoney(int money)
    {
       upgradeMoney = money;
    }

    public void UpgradeButton(GameObject go)
    {
        PlayerPrefsEnum stat = go.GetComponent<CheckPlayerPref>().statPrefs;

        int level = PlayerPrefs.GetInt(stat.ToString(), 1);

        upgradeMoney *= level;

        if (MoneyManager.instance.money >= upgradeMoney && level <= maxLevels)
        {
            EventTriggerAccept(gameObject);

            levels[level - 1].GetComponent<Image>().color = Color.yellow;

            level++;

            PlayerPrefs.SetInt(stat.ToString(), level);

            MoneyManager.instance.money -= upgradeMoney;

            PlayerPrefs.SetInt(currentMoneyPrefs.ToString(), MoneyManager.instance.money);

            Debug.Log(MoneyManager.instance.money);
        }

        else
        {
            EventTriggerDeny(gameObject);
        }
 
    }
}
