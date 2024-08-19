using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradeManager : MonoBehaviour
{
    public void UpgradeButton(string stat)
    {
        int level = PlayerPrefs.GetInt(stat, 1);

        PlayerPrefs.SetInt(stat, level++);
    }
}
