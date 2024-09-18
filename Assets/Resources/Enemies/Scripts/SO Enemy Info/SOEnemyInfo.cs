using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Stats/Enemy")]
public class SOEnemyInfo : ScriptableObject
{
    [Header("General Information")]
    [Space]
    public int id; // Identificador único del enemigo

    [Range(0f, 10f)]
    public float speed = 1; // Velocidad de movimiento del enemigo
    public float health = 10; // Cantidad de salud del enemigo

    [Header("Combat")]
    [Space]
    [Tooltip("Attack range in units (0 if melee)")]
    [Range(0f, 10f)]
    public float attackRange = 0f; // Rango de ataque en unidades (0 si es de combate cuerpo a cuerpo)
    [Range(0.1f, 10f)]
    public int damage = 1; // Daño causado por el enemigo

    [Header("Ranged Stats")]
    [Space]
    [Range(0.1f, 5f)]
    public float attackSpeed = 1; // Velocidad de ataque para enemigos que disparan
    [Tooltip("Only Ranged.")]
    [Range(1, 50)]
    public float projectileSpeed = 2; // Velocidad del proyectil para enemigos que disparan
}
