using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Stats/Player")]
public class SOPlayerInfo : ScriptableObject
{
    [Header("General Information")]
    [Range(0.1f, 50f)]
    public float health = 10;
    [Range(0f, 10f)]
    public float speed = 5;
    [Range(0f, 10f)]
    public float absortion = 1;
    [Range(0.1f, 5f)]
    public float iFrames = 2;

    [Header("Combat")]
    [Range(0.1f, 10f)]
    public int damage = 1;
    [Range(0.1f, 5f)]
    public float attackSpeed = 1;
    [Range(0.1f, 5f)]
    public float cooldown = 1;

    [Header("Range Stats")]
    [Range(1f, 50f)]
    [Tooltip("Only Ranged.")]
    public float projectileSpeed = 10;
    [Range(0, 50)]
    [Tooltip("Only Ranged.")]
    public float range = 5;

    [Header("Stats Upgrades")]
    public Dictionary<PlayerUpgradeEnum, int> statUpgrades = new Dictionary<PlayerUpgradeEnum, int>();

    private void OnEnable()
    {
        foreach (PlayerUpgradeEnum stat in System.Enum.GetValues(typeof(PlayerUpgradeEnum)))
        {
            statUpgrades[stat] = 1;
        }
    }


}
