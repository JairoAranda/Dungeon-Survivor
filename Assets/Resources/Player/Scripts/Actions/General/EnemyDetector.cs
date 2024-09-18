using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("General Config")]
    [Space]
    [SerializeField] protected LayerMask detectionLayer; // Capa para detectar enemigos

    [SerializeField] protected Transform handPosition; // Posición de la mano del jugador para la detección

    [Header("Lvl Multiplier")]
    [Space]
    [Range(2, 10)]
    [SerializeField] protected int attackSpeedMultiplier; // Multiplicador del nivel de velocidad de ataque

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum attackSpeedUpgrade; // Enum para el tipo de mejora de velocidad de ataque

    [SerializeField] private PlayerPrefsEnum attackSpeedPrefs; // Enum para las preferencias guardadas de velocidad de ataque

    private protected SOPlayerInfo sOPlayerInfo; // Referencia a la información del jugador

    private protected float shootCooldown; // Tiempo de espera entre disparos
    private protected float lastShootTime; // Último tiempo de disparo
    private protected float detectionRange; // Rango de detección de enemigos


    protected virtual void Start()
    {
        // Obtiene la información del jugador desde el componente SOFinderPlayer
        sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        // Configura el rango de detección basado en la información del jugador
        detectionRange = sOPlayerInfo.range;

        // Calcula la velocidad de ataque ajustada por el multiplicador de nivel y mejoras del jugador
        float attackspeed = sOPlayerInfo.attackSpeed * ScaleMultiplier.ScaleFactor(attackSpeedMultiplier, sOPlayerInfo.statUpgrades[attackSpeedUpgrade]);

        // Ajusta la velocidad de ataque basada en las preferencias guardadas del jugador
        attackspeed *= (float)(1 + 0.1 * PlayerPrefs.GetInt(attackSpeedPrefs.ToString(), 1) - 0.1);

        // Calcula el tiempo de enfriamiento entre disparos basado en la velocidad de ataque
        shootCooldown = 1 / attackspeed;
    }

    protected Transform DetectClosestEnemy()
    {
        // Obtiene todos los colliders en un círculo alrededor de la posición de la mano del jugador
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(handPosition.position, detectionRange, detectionLayer);
        Transform closestEnemy = null;
        float closestDistance = detectionRange; // Inicializa la distancia más cercana con el rango de detección

        foreach (var hit in hitColliders)
        {
            // Calcula la distancia desde la mano del jugador hasta el enemigo detectado
            float distance = Vector2.Distance(handPosition.position, hit.transform.position);
            // Si la distancia es menor que la distancia más cercana encontrada, actualiza el enemigo más cercano y la distancia
            if (distance < closestDistance)
            {
                closestEnemy = hit.transform;
                closestDistance = distance;
            }
        }

        return closestEnemy; // Retorna el enemigo más cercano encontrado
    }
}
