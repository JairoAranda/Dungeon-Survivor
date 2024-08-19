using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Lvl Multiplier")]
    [Range(2, 10)]
    [SerializeField] private int multiplier = 5;

    [Header("Updrade Type")]
    [SerializeField] private PlayerUpgradeEnum speedUpgrade;

    private SOPlayerInfo sOPlayerInfo;
    private float m_speed, scaleFactor;
    private Rigidbody2D rb;
    private InputHandler inputHandler;
    private Animator animator;

    private void OnEnable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat += UpgradeStat;
    }

    private void OnDisable()
    {
        RandomStatsUpgradeManager.EventTriggerOnUpgradeStat -= UpgradeStat;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
        sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;
        animator = GetComponent<Animator>();

        UpgradeStat();
    }

    void UpgradeStat()
    {
        scaleFactor = ScaleMultiplier.ScaleFactor(multiplier, sOPlayerInfo.statUpgrades[speedUpgrade]);

        m_speed = sOPlayerInfo.speed * PlayerPrefs.GetInt("Speed", 1)  * scaleFactor;
    }

    private void Update()
    {
        animator.SetFloat("Speed", scaleFactor);

        Vector2 movement = inputHandler.GetMovementInput();
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 movement)
    {
        rb.velocity = movement * m_speed;
    }
}
