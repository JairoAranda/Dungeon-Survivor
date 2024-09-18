using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMultiplier : MonoBehaviour
{
    // Método estático que calcula un factor de escala basado en un multiplicador y el nivel actual
    public static float ScaleFactor(int multiplier, float lvl)
    {
        // Calcula el factor de escala: aumenta proporcionalmente según el nivel y el multiplicador.
        // lvl - 1 representa la progresión en los niveles. El valor 19 es un divisor para suavizar el aumento del factor.
        // El multiplicador ajusta la magnitud del crecimiento.
        float scaleFactor = 1f + ((float)(lvl - 1) / 19f) * (multiplier - 1);

        return scaleFactor; // Devuelve el factor de escala resultante
    }
}
