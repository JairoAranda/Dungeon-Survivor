using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilityAsign : MonoBehaviour
{
    IAbility ability; // Referencia a la interfaz de habilidad

    [Header("Inputs")]
    [SerializeField] InputActionAsset inputActions;
    private InputAction qAbility, eAbility;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        var gameplayMap = inputActions.FindActionMap("Gameplay"); // Encuentra el mapa de acción "Gameplay"
        qAbility = gameplayMap.FindAction("QAbilities");
        qAbility.Enable();
        eAbility = gameplayMap.FindAction("EAbilities");
        eAbility.Enable();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        qAbility.Disable();
        eAbility.Disable();
    }

    private void Start()
    {
        // Asigna la habilidad al iniciar
        Assign();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Asigna la habilidad cuando se carga la escena con el índice 1
        if (scene.buildIndex == 1)
        {
            Assign();
        }
    }

    void Assign()
    {
        // Obtiene el componente IAbility en el objeto actual
        ability = gameObject.GetComponent<IAbility>();

        if (AbilitiesManager.instance.qAbility == null)
        {
            // Si no hay ninguna habilidad asignada a la tecla Q, asigna la habilidad actual
            ability.abilityAction = qAbility;
            ability.CDimg = AbilitiesManager.instance.qCdImg;
            AbilitiesManager.instance.qImg.sprite = ability.img;
            AbilitiesManager.instance.qAbility = ability;
        }
        else
        {
            // Si ya hay una habilidad asignada a la tecla Q, asigna la habilidad actual a la tecla E
            ability.abilityAction = eAbility;
            ability.CDimg = AbilitiesManager.instance.eCdImg;
            AbilitiesManager.instance.eImg.sprite = ability.img;
            AbilitiesManager.instance.eAbility = ability;
        }
    }

    private void OnDestroy()
    {
        // Limpia la asignación de habilidades en el AbilitiesManager cuando el objeto se destruye
        if (AbilitiesManager.instance.qAbility == ability)
        {
            AbilitiesManager.instance.qAbility = null;
        }

        else
        {
            AbilitiesManager.instance.eAbility = null;
        }
    }
}
