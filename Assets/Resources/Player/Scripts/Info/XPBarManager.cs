using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarManager : MonoBehaviour
{
    [SerializeField] Image XPBar; // Referencia a la imagen de la barra de experiencia (XP)

    float maxXP; // Almacena el valor máximo de experiencia (XP) necesario para el siguiente nivel

    private void OnEnable()
    {
        PlayerStats.EventTriggerLevelUp += UpdateMaxXP;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerLevelUp -= UpdateMaxXP;
    }

    private void Start()
    {
        UpdateMaxXP();
    }


    // Método que actualiza el valor máximo de experiencia (XP) en función del nivel actual del jugador
    void UpdateMaxXP()
    {
        maxXP = PlayerStats.instance.xpMax; // Obtiene el valor máximo de XP del jugador
    }

    // Método llamado en intervalos fijos de tiempo, ideal para actualizaciones constantes como una barra de progreso
    private void FixedUpdate()
    {
        // Comprueba si la instancia de PlayerStats existe
        if (PlayerStats.instance != null)
        {
            // Calcula el porcentaje de XP actual y actualiza la barra de XP
            XPBar.fillAmount = PlayerStats.instance.xp / maxXP;
        }
    }
}
