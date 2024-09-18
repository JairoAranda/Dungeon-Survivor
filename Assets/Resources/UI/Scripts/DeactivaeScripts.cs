using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivaeScripts : MonoBehaviour
{
    [SerializeField] List <MonoBehaviour> scripts; // Lista de scripts a desactivar o activar

    [SerializeField] bool deactivePlayer; // Indica si se deben desactivar los componentes del jugador

    MeleeHitController meleeHitController; // Referencia al controlador de ataque cuerpo a cuerpo

    HandMovement handMovement; // Referencia al controlador de movimiento de la mano

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
        if (meleeHitController != null)
        {
            meleeHitController.candHit = false;
        }

        // Desactiva la capacidad de apuntar del controlador de movimiento de la mano
        if (handMovement != null)
        {
            handMovement.canAim = false;
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
        if (meleeHitController != null)
        {
            meleeHitController.candHit = true;

        }

        // Restaura la capacidad de apuntar del controlador de movimiento de la mano
        if (handMovement != null)
        {
            handMovement.canAim = true;
        }
    }
}
