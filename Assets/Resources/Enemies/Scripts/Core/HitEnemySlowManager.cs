using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemySlowManager : MonoBehaviour
{
    private void OnEnable()
    {
        EnemyStats.EventTriggerHitEnemy += StarSlow;
    }

    private void OnDisable()
    {
        EnemyStats.EventTriggerHitEnemy -= StarSlow;
    }

    void StarSlow(GameObject gameObject)
    {
        HitEnemySlow hitEnemySlow = gameObject.GetComponent<HitEnemySlow>();

        // Asegúrate de que el objeto tenga el componente HitEnemySlow
        if (hitEnemySlow != null)
        {
            hitEnemySlow.StopAllCoroutines();

            StartCoroutine(hitEnemySlow.SlowAnim());
        }

    }
}
