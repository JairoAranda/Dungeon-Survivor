using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Stats/Player")]
public class SOPlayerInfo : ScriptableObject
{
    [Header("General Information")]
    [Range(0f, 10f)]
    public float speed = 1;
    public float health;
    [Range(0.1f, 5f)]
    public float iFrames;

    [Header("Combat")]
    [Range(0.1f, 10f)]
    public int damage = 1;
    [Range(0.1f, 5f)]
    public float cooldown = 1;

    [Header("General Stats")]
    [Range(1f, 20f)]
    public int luck = 1;
    [Range(1f, 20f)]
    public int absortion = 1;
    [Range(1f, 20f)]
    public int xp = 1;
    [Range(1f, 20f)]
    public int money = 1;

    [Header("Range Stats")]
    [Range(1f, 50f)]
    [Tooltip("Only Ranged.")]
    public float projectileSpeed = 2;
    [Range(0, 50)]
    [Tooltip("Only Ranged.")]
    public float range = 2;
}
