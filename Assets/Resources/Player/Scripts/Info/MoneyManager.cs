using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] PlayerPrefsEnum currentMoneyPrefs; // Clave para guardar el dinero en PlayerPrefs

    [HideInInspector]
    public int money; // Cantidad actual de dinero

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
        // Carga el dinero guardado desde PlayerPrefs o establece en 0 si no hay datos guardados
        money = PlayerPrefs.GetInt(currentMoneyPrefs.ToString(), 0);
    }

    void SaveMoney(GameObject go)
    {
        // Guarda la cantidad de dinero en PlayerPrefs al morir el jugador
        PlayerPrefs.SetInt(currentMoneyPrefs.ToString(), money);
    }
}
