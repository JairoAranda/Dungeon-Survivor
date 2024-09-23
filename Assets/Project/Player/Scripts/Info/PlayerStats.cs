using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerStats : MonoBehaviour, IStats
{
    public static PlayerStats instance;

    // Eventos que se disparan en momentos clave: recibir daño, muerte, regeneración de salud y subir de nivel
    public static event Action<GameObject> EventTriggerHitPlayer, EventTriggerDeathPlayer, EventTriggerHealthRegen;
    public static event Action EventTriggerLevelUp;

    [HideInInspector]
    public SOPlayerInfo soPlayerInfo; // Información del jugador obtenida de un ScriptableObject

    [Header("Player Tpye")]
    [Space]
    public PlayerType playerType; // Tipo de jugador

    [Header("Lvl XP")]
    [Space]
    [Range(0.1f, 500f)]
    public float xpMax = 100; // Cantidad máxima de experiencia para subir de nivel

    [Header("Lvl Multiplier")]
    [Space]
    [Range(2, 10)]
    [SerializeField] private int lifeMultiplier = 5;
    [Range(2, 10)]
    [SerializeField] private int lifeRegenMultiplier = 5;
    [Range(2, 10)]
    [SerializeField] private int dmgMultiplier = 5;
    [Range(2, 50)]
    [SerializeField] private int cooldownMultiplier = 5;

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum lifeUpgrade;
    [SerializeField] private PlayerUpgradeEnum lifeRegenUpgrade;
    [SerializeField] private PlayerUpgradeEnum dmgUpgrade;
    [SerializeField] private PlayerUpgradeEnum cdUpgrade;

    [Header("PlayerPrefs Type")]
    [Space]
    [SerializeField] private PlayerPrefsEnum lifePref;
    [SerializeField] private PlayerPrefsEnum lifeRegenPref;
    [SerializeField] private PlayerPrefsEnum dmgPref;
    [SerializeField] private PlayerPrefsEnum cdPref;

    [HideInInspector]
    public int lvl = 1; // Nivel actual del jugador

    [HideInInspector]
    public float xp; // Experiencia acumulada

    [HideInInspector]
    public float dmg; // Daño del jugador

    [HideInInspector]
    public float healthRegen; // Cantidad de regeneración de vida por segundo

    [HideInInspector]
    public float coolDownReduction; // Reducción del tiempo de reutilización (cooldown)

    [HideInInspector]
    public bool canBeHit = true; // Controla si el jugador puede recibir daño

    private float _life; // Vida actual del jugador

    public float life
    {
        get => _life;
        set
        {
            _life = value;
            if (_life <= 0 )
            {
                Death();
            }
        }
    }

    [HideInInspector]
    public float currentMaxLife, maxLife; // Vida máxima del jugador

    private bool _isDead = false; // Bandera para verificar si el jugador ha muerto

    public bool isDead
    {
        get => _isDead;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpgradeStats;
        SceneManager.sceneLoaded += DestroyPlayer;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpgradeStats;
        SceneManager.sceneLoaded -= DestroyPlayer;
    }

    private void Start()
    {
        soPlayerInfo = gameObject.GetComponent<SOFinderPlayer>().sOPlayerInfo; // Obtiene la información del jugador desde el ScriptableObject

        UpgradeStats();

        StartCoroutine(HealthRegen());
    }

    void UpgradeStats()
    {
        // Calcula las estadísticas del jugador basadas en multiplicadores y mejoras
        maxLife = soPlayerInfo.health * (float)(1 + 0.1 * PlayerPrefs.GetInt(lifePref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(lifeMultiplier, soPlayerInfo.statUpgrades[lifeUpgrade]);
        life += maxLife - currentMaxLife; // Aumenta la vida actual si el máximo ha cambiado
        currentMaxLife = maxLife;

        healthRegen = soPlayerInfo.healthRegen * (float)(1 + 0.1 * PlayerPrefs.GetInt(lifeRegenPref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(lifeRegenMultiplier, soPlayerInfo.statUpgrades[lifeRegenUpgrade]);

        dmg = soPlayerInfo.damage * (float)(1 + 0.1 * PlayerPrefs.GetInt(dmgPref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(dmgMultiplier, soPlayerInfo.statUpgrades[dmgUpgrade]);

        coolDownReduction = (float)(1 + 0.1 * PlayerPrefs.GetInt(cdPref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(cooldownMultiplier, soPlayerInfo.statUpgrades[cdUpgrade]) - 1;

    }

    void DestroyPlayer(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1 && gameObject != null)
        {
            gameObject.transform.position = Vector3.zero; // Reinicia la posición del jugador al cargar la escena 1
        }

        if (scene.buildIndex == 0)
        {
            Destroy(gameObject); // Destruye el objeto al regresar a la escena principal
        }
    }

    private void Update()
    {
        CheckXP();

    }


    public void GetHit(float dmg)
    {
        if (canBeHit) // Solo recibe daño si está permitido
        {
            life -= dmg;

            if (life > 0)
            {
                EventTriggerHitPlayer?.Invoke(gameObject); // Dispara el evento de recibir daño si no ha muerto
            }
        }
        
    }

    public void Death()
    {
        if (!_isDead) // Si aún no ha muerto
        {
            _isDead = true;

            EventTriggerDeathPlayer?.Invoke(gameObject); // Dispara el evento de muerte del jugador

        }
        
    }

    void CheckXP()
    {
        if (xp >= xpMax) // Verifica si el jugador ha acumulado suficiente experiencia para subir de nivel
        {
            lvl++; // Incrementa el nivel
            xp -= xpMax; // Restablece la experiencia al excedente
            xpMax *= 1.2f; // Aumenta el requisito de experiencia para el siguiente nivel

            EventTriggerLevelUp?.Invoke(); // Dispara el evento de subida de nivel
        }
    }

    IEnumerator HealthRegen()
    {
        // Regenera la salud periódicamente
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (life < maxLife && life > 0)  // Regenera la salud solo si está por debajo del máximo y no está muerto
            {
                life += healthRegen;
                EventTriggerHealthRegen(gameObject); // Dispara el evento de regeneración de salud
            }
        }
        
    }

}
