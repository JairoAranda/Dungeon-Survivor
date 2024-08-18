using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField] GameObject upgradeOptionGO;
    [SerializeField] GameObject chestMenu;

    private void OnEnable()
    {
        PlayerStats.EventTriggerLevelUp += EnableLevelUpOptions;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerLevelUp -= EnableLevelUpOptions;
    }

    void EnableLevelUpOptions()
    {
        while (chestMenu.activeSelf)
        {
            StartCoroutine(CheckMenu());
        }

        upgradeOptionGO.SetActive(true);
    }

    IEnumerator CheckMenu()
    {
        yield return new WaitForFixedUpdate();

        EnableLevelUpOptions();
    }
}
