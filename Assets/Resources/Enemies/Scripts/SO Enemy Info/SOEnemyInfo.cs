using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Stats/Enemy")]
public class SOEnemyInfo : ScriptableObject
{
    [Header("General Information")]
    public int id;

    [Range(0f, 10f)]
    public float speed = 1;
    public float health = 10;

    [Header("Combat")]
    [Tooltip("Attack range in units (0 if melee)")]
    [Range(0f, 10f)]
    public float attackRange = 0f;
    [Range(0.1f, 10f)]
    public int damage = 1;

    [Header("Ranged Stats")]
    [Range(0.1f, 5f)]
    public float attackSpeed = 1;
    [Tooltip("Only Ranged.")]
    [Range(1, 50)]
    public float projectileSpeed = 2;
}
