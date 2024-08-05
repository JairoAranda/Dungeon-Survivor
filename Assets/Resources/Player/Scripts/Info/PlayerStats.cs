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

    private SOPlayerInfo sOFinderPlayer;

    //[HideInInspector]
    public float xp;

    [SerializeField] private float xpMax = 100;

    //[HideInInspector]
    public int lvl = 1;

    private float _life;
    public float life
    {
        get => _life;
        set => _life = value;
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
        sOFinderPlayer = gameObject.GetComponent<SOFinderPlayer>().sOPlayerInfo;

        life = sOFinderPlayer.health;
    }

    private void Update()
    {
        CheckLife();

        CheckXP();
    }

    public void GetHit(float dmg)
    {
        life -= dmg;
       
        if (life > 0)
        {
            EventTriggerHitPlayer();
        }
    }

    void CheckLife()
    {
        if (life <= 0)
        {
            EventTriggerDeathPlayer();
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
