using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerStats : MonoBehaviour, IStats
{
    public static PlayerStats instance;

    public static event Action<GameObject> EventTriggerHitPlayer, EventTriggerDeathPlayer;

    public static event Action EventTriggerLevelUp;

    [HideInInspector]
    public SOPlayerInfo soPlayerInfo;

    [Header("Player Tpye")]
    [Space]
    public PlayerType playerType;

    [Header("Lvl XP")]
    [Space]
    [Range(0.1f, 500f)]
    public float xpMax = 100;

    [Header("Lvl Multiplier")]
    [Space]
    [Range(2, 10)]
    [SerializeField] private int lifeMultiplier = 5;
    [Range(2, 10)]
    [SerializeField] private int dmgMultiplier = 5;
    [Range(2, 50)]
    [SerializeField] private int cooldownMultiplier = 5;

    [Header("Stat Type")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum lifeUpgrade;
    [SerializeField] private PlayerUpgradeEnum dmgUpgrade;
    [SerializeField] private PlayerUpgradeEnum cdUpgrade;

    [Header("PlayerPrefs Type")]
    [Space]
    [SerializeField] private PlayerPrefsEnum lifePref;
    [SerializeField] private PlayerPrefsEnum dmgPref;
    [SerializeField] private PlayerPrefsEnum cdPref;

    [HideInInspector]
    public int lvl = 1;

    [HideInInspector]
    public float xp;

    [HideInInspector]
    public float dmg;

    [HideInInspector]
    public float coolDownReduction;

    [HideInInspector]
    public bool canBeHit = true;

    private float _life;

    [HideInInspector]
    public float currentMaxLife, maxLife;

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

    private bool _isDead = false;

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
        soPlayerInfo = gameObject.GetComponent<SOFinderPlayer>().sOPlayerInfo;

        UpgradeStats();
    }

    void UpgradeStats()
    {
        maxLife = soPlayerInfo.health * (float)(1 + 0.1 * PlayerPrefs.GetInt(lifePref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(lifeMultiplier, soPlayerInfo.statUpgrades[lifeUpgrade]);
        life += maxLife - currentMaxLife;
        currentMaxLife = maxLife;

        dmg = soPlayerInfo.damage * (float)(1 + 0.1 * PlayerPrefs.GetInt(dmgPref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(dmgMultiplier, soPlayerInfo.statUpgrades[dmgUpgrade]);

        coolDownReduction = (float)(1 + 0.1 * PlayerPrefs.GetInt(cdPref.ToString(), 1) - 0.1) * ScaleMultiplier.ScaleFactor(cooldownMultiplier, soPlayerInfo.statUpgrades[cdUpgrade]) - 1;

    }

    void DestroyPlayer(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1 && gameObject != null)
        {
            gameObject.transform.position = Vector3.zero;
        }
    }

    private void Update()
    {
        CheckXP();
    }


    public void GetHit(float dmg)
    {
        if (canBeHit)
        {
            life -= dmg;

            if (life > 0)
            {
                EventTriggerHitPlayer?.Invoke(gameObject);
            }
        }
        
    }

    public void Death()
    {
        if (!_isDead)
        {
            _isDead = true;

            EventTriggerDeathPlayer?.Invoke(gameObject);
            SceneManager.LoadScene(0);
        }
        
    }

    void CheckXP()
    {
        if (xp >= xpMax)
        {
            lvl++;
            xp -= xpMax;
            xpMax *= 1.2f;

            EventTriggerLevelUp?.Invoke();
        }
    }

}
