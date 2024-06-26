using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy")]
public class EnemyInfoSO : ScriptableObject
{
    [Header("General Information")]
    public int id;

    [Range(0f, 10f)]
    public float speed = 1;
    public int health;

    [Header("Combat")]
    [Tooltip("Attack range in units.")]
    public float attackRange;
    public int damage;
}
