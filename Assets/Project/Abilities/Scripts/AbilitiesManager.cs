using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AbilitiesManager : MonoBehaviour
{
    public static AbilitiesManager instance; // Instancia est�tica para acceso global

    [HideInInspector]
    public IAbility qAbility, eAbility; // Referencias a las habilidades asociadas a las teclas Q y E

    [Header("CD Image")] // Im�genes en la UI para mostrar el tiempo de recarga de las habilidades Q y E
    public Image qCdImg;  
    public Image eCdImg;

    [Header("Ability Image")] // Im�genes en la UI para mostrar las habilidades Q y E
    public Image qImg; 
    public Image eImg;

    [Header("CD Text")] // Textos en la UI para mostrar el tiempo de recarga en formato num�rico
    public TextMeshProUGUI qCdText; 
    public TextMeshProUGUI eCdText;

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

    private void Update()
    {
        Ability(qAbility);

        Ability(eAbility);
    }

    void Ability(IAbility ability)
    {
        if (ability != null)
        {
            // Actualiza el uso de la habilidad, la imagen de recarga y el texto de recarga
            if (qAbility != null && eAbility != null)
            {
                if (!qAbility.isUsing && !eAbility.isUsing)
                {
                    AbilityUse(ability);
                }
            }
            else
            {
                AbilityUse(ability);
            }
                
            ImgCd(ability);
            CD(ability);
        }
        
    }

    void AbilityUse(IAbility ability)
    {
        // Verifica si la tecla asociada a la habilidad ha sido presionada
        if (ability.abilityAction.triggered)
        {
            // Solo permite usar la habilidad si el tiempo de recarga es 0
            if (ability.currentCD <= 0 && !ability.isUsing)
            {
                ability.isUsing = true;

                ability.Ability(); // Llama al m�todo Ability de la habilidad
            }

        }
    }

    void ImgCd(IAbility ability)
    {
        // Actualiza la imagen de recarga para mostrar el tiempo restante
        ability.CDimg.fillAmount = ability.currentCD / (ability.cd * (1 - PlayerStats.instance.coolDownReduction / 100));
    }

    void CD(IAbility ability)
    {
        // Actualiza el texto de recarga dependiendo de si la habilidad es Q o E
        if (ability.CDimg == qCdImg)
        {
            if (!qCdText.enabled)
            {
                qCdText.enabled = true; // Muestra el texto si est� desactivado
            }
            
            qCdText.text = ability.currentCD.ToString("F1"); // Muestra el tiempo de recarga con un decimal

            if (ability.currentCD <= 0)
            {
                qCdText.enabled = false; // Oculta el texto si el tiempo de recarga ha terminado
            }
        }
        else
        {
            if (!eCdText.enabled)
            {
                eCdText.enabled = true; // Muestra el texto si est� desactivado
            }

            eCdText.text = ability.currentCD.ToString("F1"); // Muestra el tiempo de recarga con un decimal

            if (ability.currentCD <= 0)
            {
                eCdText.enabled = false; // Oculta el texto si el tiempo de recarga ha terminado
            }
        }
    }

}
