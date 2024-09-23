using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReciveDrop : MonoBehaviour
{
    // Evento est�tico para notificar que se ha recogido un �tem
    public static event Action<GameObject> EventTriggerGetItem;

    [Header("Drop type")]
    [Space]
    public EnumDropType type; // Tipo de drop (definido en un Enum)

    [Header("Stat Upgrade")]
    [Space]
    [SerializeField] private PlayerUpgradeEnum upgrade; // Enum que define qu� estad�stica se actualizar�

    [SerializeField] private PlayerPrefsEnum playerPrefs; // Enum para acceder al save

    [HideInInspector]
    public bool isPlaying; // Indica si la animaci�n est� en curso

    [Header("Stat Options")]
    [Space]
    [Range(0.1f , 5f)]
    [SerializeField] private float gatherTime = 1; // Tiempo de recolecci�n del drop

    [Range(0f, 10f)]
    [SerializeField] private float baseValue = 5; // Valor base del drop

    [Range(2f, 10f)]
    [SerializeField] private int multiplier = 5; // Multiplicador para el valor del drop

    protected float totalValue; // Valor total calculado del drop

    Tweener tweener; // Referencia al tweener de DoTween para la animaci�n

    float currentTime; // Tiempo actual durante la animaci�n

    protected SOPlayerInfo sOPlayerInfo; // Informaci�n del jugador

    private void Start()
    {
        // Obtiene la informaci�n del jugador desde la instancia de PlayerStats
        sOPlayerInfo = PlayerStats.instance.soPlayerInfo;
    }

    public void StartAnim()
    {
        currentTime = 0;

        // Inicia la animaci�n de movimiento hacia la posici�n del jugado
        tweener = transform.DOMove(PlayerStats.instance.transform.position, gatherTime)
            .SetEase(Ease.InBack) // Configura el easing para la animaci�n
            .OnUpdate(() =>
            {
                currentTime += Time.deltaTime;

                // Cambia el easing a Linear y actualiza la posici�n cuando se llega a la mitad de la animaci�n
                if (currentTime >= gatherTime/2 && currentTime <= gatherTime)
                {
                    tweener.SetEase(Ease.Linear);
                    tweener.ChangeEndValue(PlayerStats.instance.transform.position, gatherTime - currentTime ,true);
                }
            })
            .OnComplete(() => AnimDone()); // Llama a AnimDone cuando la animaci�n termina

    }

    protected virtual void AnimDone()
    {
        // Dispara el evento para notificar que se ha recogido un �tem
        EventTriggerGetItem(gameObject);

        // Calcula el valor total del drop basado en el valor base, las preferencias del jugador y el multiplicador
        totalValue = baseValue * PlayerPrefs.GetInt(playerPrefs.ToString(), 1) * ScaleMultiplier.ScaleFactor(multiplier, sOPlayerInfo.statUpgrades[upgrade]);

        StartCoroutine(DestoyObject());

    }

    protected virtual IEnumerator DestoyObject()
    {
        yield return new WaitForEndOfFrame(); 

        isPlaying = false; // Marca la animaci�n como completada

        gameObject.SetActive(false); // Desactiva el objeto del juego
    }

}
