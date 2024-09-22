using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject endScreen;

    private void OnEnable()
    {
        PlayerStats.EventTriggerDeathPlayer += EndScreen;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerDeathPlayer -= EndScreen;
    }

    void EndScreen(GameObject go)
    {
        endScreen.SetActive(true);
    }
}
