using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Stats/Player")]
public class SOPlayerInfo : ScriptableObject
{
    [Header("General Information")]
    [Space]
    [Range(0.1f, 50f)]
    public float health = 10; //Vida del jugador
    [Range(0.01f, 1f)]
    public float healthRegen = 0.01f; // Regeneración de vida
    [Range(0f, 10f)]
    public float speed = 5; // Velocidad de movimiento del jugador
    [Range(0f, 20)]
    public float absortion = 1; // Rango de absorción de objetos
    [Range(0.1f, 5f)]
    public float iFrames = 2; // Tiempo de invulnerabilidad tras recibir daño (iFrames)

    [Header("Combat")]
    [Space]
    [Range(0.1f, 10f)]
    public float damage = 1; // Daño base del jugador
    [Range(0.1f, 5f)]
    public float attackSpeed = 1; // Velocidad de ataque del jugador
    [Range(0.1f, 5f)]
    public float cooldown = 1; // Reducción de enfriamiento de habilidades

    [Header("Range Stats")]
    [Space]
    [Range(0f, 50f)]
    [Tooltip("If melee = 0")]
    [Space]
    public float projectSpeed = 10; // Velocidad del proyectil
    [Range(0, 50)]
    [Tooltip("If melee = 0")]
    [Space]
    public float range = 5; // Rango del ataque a distancia

    [Header("Stats Upgrades")]
    [Space]
    public Dictionary<PlayerUpgradeEnum, int> statUpgrades = new Dictionary<PlayerUpgradeEnum, int>();

    // Este método se llama cuando el ScriptableObject se habilita (como cuando se carga)
    private void OnEnable()
    {
        // Inicializa todas las estadísticas en el diccionario con un valor de 1
        foreach (PlayerUpgradeEnum stat in System.Enum.GetValues(typeof(PlayerUpgradeEnum)))
        {
            statUpgrades[stat] = 1;
        }
    }

}
