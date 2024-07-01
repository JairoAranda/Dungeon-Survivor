using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Stats/Player")]
public class SOPlayerInfo : ScriptableObject
{
    [Header("General Information")]
    [Range(0f, 10f)]
    public float speed = 1;
    public int health;
    [Range(0.1f, 5f)]
    public float iFrames;

    [Header("Combat")]
    [Range(0.1f, 10f)]
    public int damage = 1;
    [Range(0.1f, 5f)]
    public float cooldown = 1;
    [Range(250f, 750f)]
    [Tooltip("Only Ranged.")]
    public float projectileSpeed = 100;
}
