using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    [SerializeField] private float _cd;

    public float cd
    {
        get => _cd; 
        set => _cd = value;
    }

    private float _currentCD;

    public float currentCD
    {
        get => _currentCD;
        set => _currentCD = value;
    }

    private SOPlayerInfo _sOPlayerInfo;

    public SOPlayerInfo sOPlayerInfo
    {
        get => _sOPlayerInfo;
        set => _sOPlayerInfo = value;
    }

    private ProjectilePool _projectilePool;

    public ProjectilePool projectilePool
    {
        get => _projectilePool;
        set => _projectilePool = value;
    }

    [SerializeField] private LayerMask _enemyLayer;

    public LayerMask enemyLayer
    {
        get => _enemyLayer;
        set => _enemyLayer = value;
    }

    protected virtual void Start()
    {
        _sOPlayerInfo = GetComponentInParent<SOFinderPlayer>().sOPlayerInfo;

        _projectilePool = ProjectilePool.instance;
    }


    protected virtual void Update()
    {
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }
}
