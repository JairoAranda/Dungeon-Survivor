using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseAbility : MonoBehaviour
{
    [Header("Ability Config")]
    [Space]
    [Range(.1f, 20f)]
    [SerializeField] private float _cd;

    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] Sprite _abilityImg;

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

    public LayerMask enemyLayer
    {
        get => _enemyLayer;
        set => _enemyLayer = value;
    }

    private KeyCode _abilityKey;
    public KeyCode keycode
    {
        get => _abilityKey;
        set => _abilityKey = value;
    }

    public Sprite img
    {
        get => _abilityImg;
        set => _abilityImg = value;
    }

    Image _abilityCDImg;
    public Image CDimg
    {
        get => _abilityCDImg;
        set => _abilityCDImg = value;
    }

    GameObject _go;

    public GameObject go
    {
        get => _go;
        set => _go = value;
    }


    protected virtual void Start()
    {
        _sOPlayerInfo = GetComponentInParent<SOFinderPlayer>().sOPlayerInfo;

        go = gameObject;
    }


    protected virtual void Update()
    {
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }
}
