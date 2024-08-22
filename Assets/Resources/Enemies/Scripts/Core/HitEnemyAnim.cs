using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyAnim : MonoBehaviour
{
    bool isPlaying;

    public IEnumerator HitAnim(float blinkDuration, float blinkFrequency, Renderer objectRenderer, Color originalColor, Color hitColor)
    {
        if (!isPlaying)
        {
            isPlaying = true;

            float elapsedTime = 0.0f;

            while (elapsedTime < blinkDuration)
            {
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

            objectRenderer.material.color = originalColor;

            isPlaying = false;
        }
        
    }
}
