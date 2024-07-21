using System.Collections;
using System.Collections.Generic;
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

    private AbilitiesPoolManager _abilitiesManager;

    public AbilitiesPoolManager abilitiesManager
    {
        get => _abilitiesManager;
        set => _abilitiesManager = value;
    }

    protected virtual void Start()
    {
        _sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        _abilitiesManager = AbilitiesPoolManager.Instance;
    }

}
