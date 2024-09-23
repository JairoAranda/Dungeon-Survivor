using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeactivaeScripts : MonoBehaviour
{
    [SerializeField] List <MonoBehaviour> scripts; // Lista de scripts a desactivar o activar

    [SerializeField] bool deactivePlayer, deactiveInputs; // Indica si se deben desactivar los componentes del jugador

    [SerializeField] InputActionAsset inputActions;

    private InputActionMap gameplayMap;

    MeleeHitController meleeHitController; // Referencia al controlador de ataque cuerpo a cuerpo

    HandMovement handMovement; // Referencia al controlador de movimiento de la mano

    bool canHitChanged, canAimChanged;

    private void Awake()
    {
        if (deactivePlayer)
        {
            // Obtén el GameObject del jugador a través de PlayerStats
            GameObject player = PlayerStats.instance.gameObject;

            // Añade componentes específicos de jugador a la lista de scripts
            scripts.Add(player.GetComponent<ShootController>());
            meleeHitController = player.GetComponent<MeleeHitController>();
            handMovement = player.GetComponentInChildren<HandMovement>();
            scripts.Add(player.GetComponent<PlayerAnimation>());
        }

        if (deactiveInputs)
        {
            gameplayMap = inputActions.FindActionMap("Gameplay");
        }

    }

    private void OnEnable()
    {
        // Desactiva todos los scripts especificados
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }

        // Desactiva la capacidad de ataque del controlador de ataque cuerpo a cuerpo
        if (meleeHitController != null && meleeHitController.candHit)
        {
            meleeHitController.candHit = false;
            canHitChanged = true;
        }

        // Desactiva la capacidad de apuntar del controlador de movimiento de la mano
        if (handMovement != null && handMovement.canAim)
        {
            handMovement.canAim = false;
            canAimChanged = true;
        }

        if (gameplayMap != null)
        {
            gameplayMap.Disable();
        }
    }

    private void OnDisable()
    {
        // Activa todos los scripts especificados
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        // Restaura la capacidad de ataque del controlador de ataque cuerpo a cuerpo
        if (meleeHitController != null && canHitChanged)
        {
            meleeHitController.candHit = true;

        }

        // Restaura la capacidad de apuntar del controlador de movimiento de la mano
        if (handMovement != null && canAimChanged)
        {
            handMovement.canAim = true;
        }

        if (gameplayMap != null)
        {
            gameplayMap.Enable();
        }
    }
}
