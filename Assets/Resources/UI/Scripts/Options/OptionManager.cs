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

    private void Awake()
    {
        CheckToggle();

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Actualiza el valor de isAuto basado en el estado del toggle.
    public void CheckToggle()
    {
        isAuto = toggle.isOn;
    }

}
