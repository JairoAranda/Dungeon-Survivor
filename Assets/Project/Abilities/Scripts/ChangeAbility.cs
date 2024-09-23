using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAbility : MonoBehaviour
{
    // Método para cambiar la habilidad asignada a la tecla Q
    public void ChangeQ()
    {
        // Elimina la habilidad actualmente asignada a la tecla Q
        Destroy(AbilitiesManager.instance.qAbility.go);

        gameObject.SetActive(false);
    }

    // Método para cambiar la habilidad asignada a la tecla E
    public void ChangeE()
    {
        // Elimina la habilidad actualmente asignada a la tecla E
        Destroy(AbilitiesManager.instance.eAbility.go);

        gameObject.SetActive(false);
    }
}
