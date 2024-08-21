using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerStats : MonoBehaviour, IStats
{
    public static PlayerStats instance;

    public static event Action EventTriggerHitPlayer, EventTriggerDeathPlayer, EventTriggerLevelUp;

    private SOPlayerInfo soPlayerInfo;

    [Header("Player Tpye")]
    public PlayerType playerType;

    [Header("Lvl XP")]
    [Range(0.1f, 500f)]
    [SerializeField] private float xpMax = 100;

    [Header("Lvl Multiplier")]
    [Range(2, 10)]
    [SerializeField] private int lifeMultiplier = 5;
    [Range(2, 10)]
    [SerializeField] private int dmgMultiplier = 5;
    [Range(2, 10)]
    [SerializeField] private int cooldownMultiplier = 5;

    [Header("Stat Type")]
    [SerializeField] private PlayerUpgradeEnum lifeUpgrade;
    [SerializeField] private PlayerUpgradeEnum dmgUpgrade;
    [SerializeField] private PlayerUpgradeEnum cdUpgrade;

    [HideInInspector]
    public int lvl = 1;

    [HideInInspector]
    public float xp;

    [HideInInspector]
    public float dmg;

    [HideInInspector]
    public float coolDownReduction;

    private float _life;

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
    }

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpgradeStats;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpgradeStats;
    }

    private void Start()
    {
        soPlayerInfo = gameObject.GetComponent<SOFinderPlayer>().sOPlayerInfo;

        UpgradeStats();
    }

    void UpgradeStats()
    {
        life = soPlayerInfo.health * PlayerPrefs.GetInt("Health", 1) * ScaleMultiplier.ScaleFactor(lifeMultiplier, soPlayerInfo.statUpgrades[lifeUpgrade]);
        dmg = soPlayerInfo.damage * PlayerPrefs.GetInt("Damage", 1) * ScaleMultiplier.ScaleFactor(dmgMultiplier, soPlayerInfo.statUpgrades[dmgUpgrade]);
        coolDownReduction = PlayerPrefs.GetInt("CD", 1) * ScaleMultiplier.ScaleFactor(cooldownMultiplier, soPlayerInfo.statUpgrades[cdUpgrade]);
    }


    private void Update()
    {
        CheckXP();
    }

    public void GetHit(float dmg)
    {
        life -= dmg;
       
        if (life > 0)
        {
            EventTriggerHitPlayer?.Invoke();
        }
    }

    public void Death()
    {
        if (!_isDead)
        {
            _isDead = true;

            EventTriggerDeathPlayer?.Invoke();
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
