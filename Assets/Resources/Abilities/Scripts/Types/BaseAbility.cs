using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AbilityAsign))]
public class BaseAbility : MonoBehaviour
{
    [Header("Ability Config")]
    [Space]
    [Range(.1f, 20f)]
    [SerializeField] private float _cd; // Tiempo de recarga de la habilidad en segundos
    public float cd
    {
        get => _cd;
        set => _cd = value;
    }

    [SerializeField] private LayerMask _enemyLayer; // Capa que representa a los enemigos
    public LayerMask enemyLayer
    {
        get => _enemyLayer;
        set => _enemyLayer = value;
    }

    [SerializeField] Sprite _abilityImg; // Imagen asociada con la habilidad
    public Sprite img
    {
        get => _abilityImg;
        set => _abilityImg = value;
    }

    private float _currentCD; // Tiempo de recarga actual
    public float currentCD
    {
        get => _currentCD;
        set => _currentCD = value;
    }

    private SOPlayerInfo _sOPlayerInfo; // Información del jugador
    public SOPlayerInfo sOPlayerInfo
    {
        get => _sOPlayerInfo;
        set => _sOPlayerInfo = value;
    }

    Image _abilityCDImg; // Imagen que muestra el tiempo de recarga en la UI
    public Image CDimg
    {
        get => _abilityCDImg;
        set => _abilityCDImg = value;
    }

    GameObject _go; // Referencia al objeto de juego
    public GameObject go
    {
        get => _go;
        set => _go = value;
    }

    private InputAction _abilityAction;

    public InputAction abilityAction //Acción de usar abilidad
    {
        get => _abilityAction;
        set => _abilityAction = value;
    }



    protected virtual void Start()
    {
        // Obtiene la información del jugador desde el componente SOFinderPlayer en el padre
        _sOPlayerInfo = GetComponentInParent<SOFinderPlayer>().sOPlayerInfo;

        // Asigna el objeto de juego actual
        go = gameObject;
    }


    protected virtual void Update()
    {
        // Actualiza el tiempo de recarga si es mayor a 0
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }
}
