using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;


    [HideInInspector]
    public bool isAuto; // Indica si el apuntado automático está habilitado o no.

    [Header("Aim Options")]
    [SerializeField] Toggle toggle;

    [SerializeField] RandomSound sound;

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

    }

    private void Start()
    {
        StartCoroutine(LaterStart());
    }

    IEnumerator LaterStart()
    {
        yield return new WaitForEndOfFrame();

        //Comprobar PlayerPref de auto
        if (PlayerPrefs.GetInt("Auto", 0) == 0)
        {
            toggle.isOn = false;
        }

        else
        {
            toggle.isOn = true;
        }

        isAuto = toggle.isOn;
    }

    // Actualiza el valor de isAuto basado en el estado del toggle.
    public void CheckToggle()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("Auto", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Auto", 0);
        }

        sound.SelectSound();

        isAuto = toggle.isOn;
    }

}
