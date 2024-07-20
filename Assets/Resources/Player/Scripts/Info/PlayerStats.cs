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
        if (life <= 0)
        {
            EventTriggerDeathPlayer();
            SceneManager.LoadScene(0);
        }
    }

    public void Hit(float dmg)
    {
        life -= dmg;
       
        if (life > 0)
        {
            EventTriggerHitPlayer();
        }
    }

}
