using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerStats : MonoBehaviour, IStats
{
    public static PlayerStats instance;

    public static event Action EventTriggerHitPlayer, EventTriggerDeathPlayer;

    private SOPlayerInfo soPlayerInfo;

    [HideInInspector]
    public float xp;

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

    [HideInInspector]
    public int lvl = 1;

    [HideInInspector]
    public float dmg;

    [HideInInspector]
    public float coolDown;

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

    private void Start()
    {
        soPlayerInfo = gameObject.GetComponent<SOFinderPlayer>().sOPlayerInfo;

        life = soPlayerInfo.health * ScaleMultiplier.scaleFactor(lifeMultiplier, soPlayerInfo.healthLvl);
        dmg = soPlayerInfo.damage * ScaleMultiplier.scaleFactor(dmgMultiplier, soPlayerInfo.dmgLvl);
        coolDown = soPlayerInfo.cooldown * ScaleMultiplier.scaleFactor(cooldownMultiplier, soPlayerInfo.cooldownLvl);
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
        if (_isDead)
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
            xp = 0;
            xpMax *= 1.2f;
        }
    }

}
