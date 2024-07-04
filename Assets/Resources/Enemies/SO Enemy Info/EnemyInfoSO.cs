using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Stats/Enemy")]
public class EnemyInfoSO : ScriptableObject
{
    [Header("General Information")]
    public int id;

    [Range(0f, 10f)]
    public float speed = 1;
    public int health = 10;

    [Header("Combat")]
    [Tooltip("Attack range in units.")]
    [Range(0.5f, 10f)]
    public float attackRange = 0.5f;
    [Range(0.1f, 10f)]
    public int damage = 1;

    [Header("Ranged Stats")]
    [Range(0.1f, 5f)]
    public float cooldown = 1;
    [Tooltip("Only Ranged.")]
    [Range(250f, 750f)]
    public float projectileSpeed = 100;
}
