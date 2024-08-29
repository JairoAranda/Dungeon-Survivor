using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void OnEnable()
    {
        PlayerStats.EventTriggerDeathPlayer += SaveMoney;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerDeathPlayer -= SaveMoney;
    }


    private void Start()
    {
        money = PlayerPrefs.GetInt("CurrentMoney", 0);
    }

    void SaveMoney(GameObject go)
    {
        Debug.Log("hola");

        PlayerPrefs.SetInt("CurrentMoney", money);
    }
}
