using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBarManager : MonoBehaviour
{
    [SerializeField] Image XPBar; // Referencia a la imagen de la barra de experiencia (XP)

    float maxXP; // Almacena el valor m�ximo de experiencia (XP) necesario para el siguiente nivel

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


    // M�todo que actualiza el valor m�ximo de experiencia (XP) en funci�n del nivel actual del jugador
    void UpdateMaxXP()
    {
        maxXP = PlayerStats.instance.xpMax; // Obtiene el valor m�ximo de XP del jugador
    }

    // M�todo llamado en intervalos fijos de tiempo, ideal para actualizaciones constantes como una barra de progreso
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
