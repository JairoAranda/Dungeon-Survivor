using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedHit : BaseMeleeAbility, IAbility
{
    [Header("Swing Config")]
    [Space]
    [Range(1f, 89f)]
    [SerializeField] float swingAngle; // �ngulo de oscilaci�n del golpe
    [Range(0.1f, 1f)]
    [SerializeField] float duration; // Duraci�n de la animaci�n del golpe

    [Header("Charge Config")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color swordColor; // Color al que se transformar� la espada durante la carga
    [Range(0.1f, 3f)]
    [SerializeField] float chargeTime = 1.5f; // Tiempo de carga antes de realizar el golpe
    [Range(1f, 4f)]
    [SerializeField] float chargeMultiplier = 2f; // Multiplicador de da�o al finalizar la carga

    SpriteRenderer spriteRenderer; // Componente SpriteRenderer para cambiar el color de la espada

    Color initialColor; // Color inicial de la espada

    private Tween rotationTween; // Tween para gestionar la rotaci�n durante el golpe

    public void Ability()
    {
        // Configura los atributos necesarios para el golpe
        Atributes();

        // Inicia la corrutina para gestionar el golpe
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        meleeDmg.canDmg = false; // Desactiva el da�o mientras se carga el golpe

        // Obtiene el componente SpriteRenderer del objeto de da�o cuerpo a cuerpo
        spriteRenderer = meleeDmg.GetComponent<SpriteRenderer>();

        // Almacena el color inicial de la espada
        initialColor = spriteRenderer.material.color;

        // Calcula el �ngulo final de rotaci�n
        float endRotation = armPosition.transform.localEulerAngles.z + swingAngle;

        // Establece el da�o inicial del golpe
        meleeDmg.dmg = PlayerStats.instance.dmg;

        // Inicializa el tween para rotar el brazo al �ngulo inicial
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        float elapsedTime = 0f;

        // Interpola el color de la espada durante el tiempo de carga
        while (elapsedTime < chargeTime)
        {
            spriteRenderer.material.color = Color.Lerp(initialColor, swordColor, elapsedTime / chargeTime);


            elapsedTime += Time.deltaTime;


            yield return null;
        }

        // Aplica el multiplicador de da�o despu�s de la carga
        meleeDmg.dmg = meleeDmg.dmg * chargeMultiplier;

        meleeDmg.canDmg = true; // Habilita el da�o

        // Inicializa el tween para rotar el brazo al �ngulo final con una animaci�n suave
        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0, 0, endRotation), duration)
            .SetEase(Ease.InOutCirc) // Configura el tipo de easing para la animaci�n
            .OnComplete(() => SwingDone()); // Llama a SwingDone() cuando la animaci�n termina
    }

    protected override void Update()
    {
        base.Update();

        if (rotationTween != null)
        {
            if (Time.timeScale == 0 && rotationTween.IsPlaying())
            {
                // Pausa la animaci�n si Time.timeScale es 0 y la animaci�n est� en marcha
                rotationTween.Pause();
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animaci�n si Time.timeScale es 1 y la animaci�n est� pausada
                rotationTween.Play();
            }
        }

    }

    void SwingDone()
    {
        // Restaura el da�o del golpe a su valor original
        meleeDmg.dmg = meleeDmg.dmg / chargeMultiplier;

        meleeDmg.canDmg = false; // Desactiva el da�o

        handMovement.enabled = true; // Habilita el movimiento de la mano

        // Restaura el color inicial de la espada
        spriteRenderer.material.color = initialColor;

        // Restaura el �ngulo inicial del brazo
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        // Habilita la capacidad de golpear en MeleeHitController
        MeleeHitController.instance.candHit = true;
    }
}
