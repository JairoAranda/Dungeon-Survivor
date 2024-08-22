using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemyAnimManager : MonoBehaviour
{
    [Header("Animation Config")]
    [SerializeField] Color hitColor = Color.red;

    [Space]
    [Range(0.1f, 5f)]
    [SerializeField] float blinkDuration = 2f;

    [Range(0.01f, 1f)]
    [SerializeField] float blinkFrequency = 0.2f;


    private Renderer objectRenderer;
    private Color originalColor;

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

        objectRenderer = gameObject.GetComponent<Renderer>();

        originalColor = objectRenderer.material.color;

        StartCoroutine(hitEnemyAnim.HitAnim(blinkDuration, blinkFrequency, objectRenderer, originalColor, hitColor));
    }


}

