using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI level; // Texto que muestra el nivel inicial y el nivel final del jugador.
    [SerializeField] private TextMeshProUGUI kills; // Texto que muestra la cantidad de enemigos eliminados.

    [SerializeField] EndScreenStatsManager endScreenStatsManager; // Referencia al administrador de estadísticas de la pantalla final.

    private void Start()
    {
        // Muestra el nivel inicial y el nivel final del jugador
        level.text = "LVL " + endScreenStatsManager.startLevel + " -> LVL " + (endScreenStatsManager.startLevel + endScreenStatsManager.levelUp);

        // Muestra la cantidad de enemigos eliminados
        kills.text = endScreenStatsManager.enemyKilled + " kills";
    }
}
