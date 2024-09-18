using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField] float duration = 0.1f; // Duración del efecto de pausa en segundos

    bool waiting; // Indica si la pausa está en curso

    private void OnEnable()
    {
        PlayerStats.EventTriggerHitPlayer += StopTime;
    }

    private void OnDisable()
    {
        PlayerStats.EventTriggerHitPlayer -= StopTime;
    }


    void StopTime(GameObject go)
    {
        // Inicia la corutina si no se está esperando actualmente
        if (!waiting)
        {
            StartCoroutine(HitStopCoroutine());
        }
    }

    private IEnumerator HitStopCoroutine()
    {
        Time.timeScale = 0f;

        waiting = true;

        // Espera durante la duración especificada en tiempo real (ignora el `Time.timeScale`)
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;

        waiting = false;
    }
}
