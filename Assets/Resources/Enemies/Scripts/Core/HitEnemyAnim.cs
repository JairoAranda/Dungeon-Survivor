using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyAnim : MonoBehaviour
{
    bool isPlaying; // Indica si la animación de golpe está en curso

    public IEnumerator HitAnim(float blinkDuration, float blinkFrequency, Renderer objectRenderer, Color originalColor, Color hitColor)
    {
        if (!isPlaying)
        {
            isPlaying = true; // Marca la animación como en curso

            float elapsedTime = 0.0f;

            // Mientras el tiempo transcurrido sea menor que la duración total del parpadeo
            while (elapsedTime < blinkDuration)
            {
                // Alterna el color del objeto entre el color original y el color de golpe
                if (objectRenderer.material.color == originalColor)
                {
                    objectRenderer.material.color = hitColor;
                }
                else
                {
                    objectRenderer.material.color = originalColor;
                }

                yield return new WaitForSeconds(blinkFrequency);

                elapsedTime += blinkFrequency;
            }

            // Asegura que el color del objeto vuelva al color original al final de la animación
            objectRenderer.material.color = originalColor;

            isPlaying = false; // Marca la animación como completada
        }
        
    }
}
