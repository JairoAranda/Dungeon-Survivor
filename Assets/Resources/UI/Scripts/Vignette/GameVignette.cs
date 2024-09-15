using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVignette : MonoBehaviour
{
    Image vignette;

    private void Start()
    {
        vignette = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        float lifePercentage = PlayerStats.instance.life / PlayerStats.instance.maxLife;

        // Usamos switch con expresiones para asignar el alpha según el rango
        float newAlpha = lifePercentage switch
        {
            <= 0.05f => 0.30f,
            <= 0.10f => 0.25f,
            <= 0.15f => 0.20f,
            <= 0.20f => 0.15f,
            <= 0.25f => 0.10f,
            <= 0.30f => 0.05f,
            _ => vignette.color.a // Mantener el valor actual si no entra en ningún rango
        };

        // Solo actualizar si el alpha cambia
        if (Mathf.Abs(vignette.color.a - newAlpha) > 0.01f)
        {
            Color currentColor = vignette.color;
            currentColor.a = newAlpha;
            vignette.color = currentColor;
        }

    }
}
