using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyAnimManager : MonoBehaviour
{
    [Header("Animation Config")]
    [Space]
    [SerializeField] Color hitColor = Color.red; // Color que el objeto tomará al ser golpeado

    [Range(0.1f, 5f)]
    [SerializeField] float blinkDuration = 2f; // Duración total de la animación de parpadeo

    [Range(0.01f, 1f)]
    [SerializeField] float blinkFrequency = 0.2f; // Frecuencia de parpadeo


    private Renderer objectRenderer; // Renderer del objeto para cambiar su color
    private Color originalColor; // Color original del objeto


    private void OnEnable()
    {
        EnemyStats.EventTriggerHitEnemy += StartAnim;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerHitEnemy -= StartAnim;
    }


    void StartAnim(GameObject gameObject)
    {
        HitEnemyAnim hitEnemyAnim = gameObject.GetComponent<HitEnemyAnim>();

        // Asegúrate de que el objeto tenga el componente HitEnemyAnim
        if (hitEnemyAnim != null)
        {
            objectRenderer = gameObject.GetComponent<Renderer>();

            // Asegúrate de que el objeto tenga un Renderer
            if (objectRenderer != null)
            {
                originalColor = objectRenderer.material.color;

                // Inicia la animación de golpe
                StartCoroutine(hitEnemyAnim.HitAnim(blinkDuration, blinkFrequency, objectRenderer, originalColor, hitColor));
            }
        }
        
    }


}

