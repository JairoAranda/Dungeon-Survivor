using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistanceToPlayer))]
[RequireComponent(typeof(SOFinderEnemy))]
public class EnemyShoot : MonoBehaviour
{

    // Evento que se dispara cuando el enemigo dispara
    public static event Action<GameObject> EventTriggerEnemyShoot;

    [Header("Shoot Config")]
    [Space]
    [SerializeField] LayerMask playerLayer; // Máscara de capa para identificar al jugador
    [ColorUsage(true, true)]
    [SerializeField] Color bulletColor; // Color de la bala

    private DistanceToPlayer distanceToPlayer; // Referencia al componente DistanceToPlayer
    private SOEnemyInfo enemyInfoSO; // Referencia a la información del enemigo

    private float shootInterval; // Intervalo entre disparos
    private float shootTimer; // Temporizador para controlar el intervalo de disparo
    void Start()
    {
        // Inicializa las referencias a los componentes y la información del enemigo
        distanceToPlayer = GetComponent<DistanceToPlayer>();
        enemyInfoSO = GetComponent<SOFinderEnemy>().enemyInfoSO;

        // Establece el intervalo de disparo basado en la velocidad de ataque del enemigo
        shootInterval = enemyInfoSO.attackSpeed;
    }

    void Update()
    {
        // Actualiza el temporizador del disparo
        shootTimer += Time.deltaTime;

        // Verifica si el enemigo está cerca del jugador y si es el momento de disparar
        if (distanceToPlayer.NearPlayer() && shootTimer >= shootInterval)
        {
            // Dispara el evento de disparo de enemigo
            EventTriggerEnemyShoot(gameObject);

            // Dispara una bala desde la posición del enemigo hacia el jugador
            ProjectilePool.instance.ShootBullet(
                gameObject.transform.position,
                enemyInfoSO.projectileSpeed,
                enemyInfoSO.damage,
                enemyInfoSO.attackRange + 40,
                PlayerStats.instance.transform.position,
                transform.position,
                playerLayer,
                gameObject,
                bulletColor
            );

            // Reinicia el temporizador de disparo
            shootTimer = 0;
        }
    }
}
