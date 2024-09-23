using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVignette : MonoBehaviour
{
    Image vignette; // Componente de imagen que representa el efecto de viñeta

    private void Start()
    {
        // Obtiene el componente de imagen del objeto
        vignette = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        // Calcula el porcentaje de vida restante del jugador
        float lifePercentage = PlayerStats.instance.life / PlayerStats.instance.maxLife;

        // Usa una expresión switch para asignar el valor de alpha basado en el porcentaje de vida
        float newAlpha = lifePercentage switch
        {
            <= 0.05f => 0.30f, // Vida muy baja: alpha alto
            <= 0.10f => 0.25f, // Vida baja: alpha alto
            <= 0.15f => 0.20f, // Vida baja-moderada: alpha medio
            <= 0.20f => 0.15f, // Vida moderada: alpha bajo
            <= 0.25f => 0.10f, // Vida un poco por debajo del promedio: alpha bajo
            <= 0.30f => 0.05f, // Vida cerca del promedio: alpha mínimo
            _ => vignette.color.a // Mantiene el valor actual de alpha si no se encuentra en ningún rango
        };

        // Solo actualiza el color si el alpha cambia significativamente
        if (Mathf.Abs(vignette.color.a - newAlpha) > 0.01f)
        {
            Color currentColor = vignette.color;
            currentColor.a = newAlpha;
            vignette.color = currentColor;
        }

    }
}
