using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;


    //[HideInInspector]
    public bool isAuto;

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

    public void CheckToggle()
    {
        isAuto = toggle.isOn;
    }

}
