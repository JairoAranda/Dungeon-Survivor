using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SOFinderPlayer))]
public class MeleeHitController : MonoBehaviour
{
    public static event Action<GameObject> EventTriggerSwing; // Evento para notificar que se ha realizado un golpe

    public static MeleeHitController instance;

    [Header("General Config")]
    [Space]
    public GameObject armPosition; // Referencia al objeto que representa la posici�n del brazo

    public HandMovement handMovement; // Referencia al controlador de movimiento de la mano

    [Header("Swing Config")]
    [Space]
    [Range(1f, 89f)]
    [SerializeField] float swingAngle; // �ngulo de swing del ataque
    [Range(0.1f, 1f)]
    [SerializeField] float duration; // Duraci�n del swing

    [Header("Inputs")]
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] InputActionEnum action;
    private InputAction shootAction;

    private float currentCD; // Tiempo de cooldown entre swings

    [HideInInspector]
    public IEffectType effect; // Tipo de efecto del golpe

    SOPlayerInfo playerInfo; // Informaci�n del jugador

    [HideInInspector]
    public MeleeDmg meleeDmg; // Referencia al componente de da�o cuerpo a cuerpo

    [HideInInspector]
    public bool candHit = true; // Indica si se puede golpear

    private void OnEnable()
    {
        var gameplayMap = inputActions.FindActionMap("Gameplay");

        shootAction = gameplayMap.FindAction(action.ToString());

        shootAction.Enable();
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
        // Inicializa las referencias y el cooldown inicial
        playerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;
        meleeDmg = GetComponentInChildren<MeleeDmg>();
        effect = GetComponentInChildren<IEffectType>();
        currentCD = playerInfo.attackSpeed;
    }


    private void Update()
    {
        Timer();

        // Realiza un golpe manual si el modo autom�tico no est� activado
        if (!OptionManager.instance.isAuto)
        {
            ManualHit();
        }
    }

    void Timer()
    {
        // Reduce el cooldown en funci�n del tiempo transcurrido
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }

    public void AutoHit()
    {
        // Realiza un golpe autom�tico si el cooldown ha terminado y se puede golpear
        if (currentCD <= 0 && candHit)
        {
            Swing();

            currentCD = playerInfo.attackSpeed; // Reinicia el cooldown
        }
    }

    void ManualHit()
    {
        // Realiza un golpe manual si el cooldown ha terminado, se hace clic y se puede golpear
        if (currentCD <= 0 && shootAction.triggered && candHit)
        {
            Swing();

            currentCD = playerInfo.attackSpeed; // Reinicia el cooldown
        }
    }

    void Swing()
    {
        // Notifica que se ha realizado un swing
        EventTriggerSwing(gameObject);

        // Desactiva y reactiva el collider para actualizar el da�o
        BoxCollider2D boxCollider2D = meleeDmg.gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        boxCollider2D.enabled = true;

        // Calcula el �ngulo final del swing
        float endRotation = armPosition.transform.localEulerAngles.z + swingAngle;

        // Desactiva el movimiento de la mano y habilita el da�o
        handMovement.enabled = false;
        meleeDmg.canDmg = true;
        meleeDmg.effectType = effect;
        meleeDmg.dmg = PlayerStats.instance.dmg;

        // Realiza la animaci�n del swing usando DOTween
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, endRotation), duration)
            .SetEase(Ease.InOutCirc)
            .OnComplete(() => SwingDone());
    }

    void SwingDone()
    {
        // Restaura el estado despu�s de que el swing ha terminado
        meleeDmg.canDmg = false;
        handMovement.enabled = true;
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);
    }
}
