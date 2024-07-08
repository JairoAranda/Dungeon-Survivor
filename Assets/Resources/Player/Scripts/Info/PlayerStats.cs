using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public static event Action EventTriggerHit, EventTriggerDeath;

    [HideInInspector]
    public GameObject player;

    private SOPlayerInfo sOFinderPlayer;
    private float life;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = gameObject;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sOFinderPlayer = player.GetComponent<SOFinderPlayer>().sOPlayerInfo;

        life = sOFinderPlayer.health;
    }

    public void Hit(float dmg)
    {
        life -= dmg;

        if (life <= 0)
        {
            EventTriggerDeath();
            SceneManager.LoadScene(0);
        }
        else
        {
            EventTriggerHit();
        }
    }

}
