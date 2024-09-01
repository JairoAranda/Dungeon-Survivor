using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField] float duration = 0.1f;

    bool waiting;

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
        if (!waiting)
        {
            StartCoroutine(HitStopCoroutine());
        }
    }

    private IEnumerator HitStopCoroutine()
    {
        Time.timeScale = 0f;

        waiting = true;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;

        waiting = false;
    }
}
