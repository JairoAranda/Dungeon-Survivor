using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgradeManager : MonoBehaviour
{
    int upgradeMoney;

    int maxLevels;

    GameObject[] levels;

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

    public void UpgradeButton(string stat)
    {
        int level = PlayerPrefs.GetInt(stat, 1);

        upgradeMoney *= level;

        if (MoneyManager.money >= upgradeMoney && level <= maxLevels)
        {
            levels[level - 1].GetComponent<Image>().color = Color.yellow;

            level++;

            PlayerPrefs.SetInt(stat, level);

            MoneyManager.money -= upgradeMoney;

            Debug.Log(MoneyManager.money);
        }
 
    }
}
