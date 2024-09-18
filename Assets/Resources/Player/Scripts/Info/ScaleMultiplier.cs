using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMultiplier : MonoBehaviour
{
    // M�todo est�tico que calcula un factor de escala basado en un multiplicador y el nivel actual
    public static float ScaleFactor(int multiplier, float lvl)
    {
        // Calcula el factor de escala: aumenta proporcionalmente seg�n el nivel y el multiplicador.
        // lvl - 1 representa la progresi�n en los niveles. El valor 19 es un divisor para suavizar el aumento del factor.
        // El multiplicador ajusta la magnitud del crecimiento.
        float scaleFactor = 1f + ((float)(lvl - 1) / 19f) * (multiplier - 1);

        return scaleFactor; // Devuelve el factor de escala resultante
    }
}
