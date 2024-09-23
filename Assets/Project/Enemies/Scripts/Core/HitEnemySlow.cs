using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitEnemySlow : MonoBehaviour
{
    [Header("Animation Config")]
    [Space]
    [Range(0.1f, 5f)]
    [SerializeField] float slowDuration = 0.5f; // Duración total de la ralentización

    SOEnemyInfo enemy;
    EnemyMovement enemyMovement;

    private void Start()
    {
        enemy = GetComponent<SOFinderEnemy>().enemyInfoSO;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public IEnumerator SlowAnim()
    {
        float elapsedTime = 0.0f;

        enemyMovement.currentSpeed = 0;

        // Mientras el tiempo transcurrido sea menor que la duración total del parpadeo
        while (elapsedTime < slowDuration)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        enemyMovement.currentSpeed = enemy.speed;
    }
}
