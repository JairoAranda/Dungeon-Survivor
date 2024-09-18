using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedHit : BaseMeleeAbility, IAbility
{
    [Header("Swing Config")]
    [Space]
    [Range(1f, 89f)]
    [SerializeField] float swingAngle; // Ángulo de oscilación del golpe
    [Range(0.1f, 1f)]
    [SerializeField] float duration; // Duración de la animación del golpe

    [Header("Charge Config")]
    [Space]
    [ColorUsage(true, true)]
    [SerializeField] Color swordColor; // Color al que se transformará la espada durante la carga
    [Range(0.1f, 3f)]
    [SerializeField] float chargeTime = 1.5f; // Tiempo de carga antes de realizar el golpe
    [Range(1f, 4f)]
    [SerializeField] float chargeMultiplier = 2f; // Multiplicador de daño al finalizar la carga

    SpriteRenderer spriteRenderer; // Componente SpriteRenderer para cambiar el color de la espada

    Color initialColor; // Color inicial de la espada

    private Tween rotationTween; // Tween para gestionar la rotación durante el golpe

    public void Ability()
    {
        // Configura los atributos necesarios para el golpe
        Atributes();

        // Inicia la corrutina para gestionar el golpe
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        meleeDmg.canDmg = false; // Desactiva el daño mientras se carga el golpe

        // Obtiene el componente SpriteRenderer del objeto de daño cuerpo a cuerpo
        spriteRenderer = meleeDmg.GetComponent<SpriteRenderer>();

        // Almacena el color inicial de la espada
        initialColor = spriteRenderer.material.color;

        // Calcula el ángulo final de rotación
        float endRotation = armPosition.transform.localEulerAngles.z + swingAngle;

        // Establece el daño inicial del golpe
        meleeDmg.dmg = PlayerStats.instance.dmg;

        // Inicializa el tween para rotar el brazo al ángulo inicial
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        float elapsedTime = 0f;

        // Interpola el color de la espada durante el tiempo de carga
        while (elapsedTime < chargeTime)
        {
            spriteRenderer.material.color = Color.Lerp(initialColor, swordColor, elapsedTime / chargeTime);


            elapsedTime += Time.deltaTime;


            yield return null;
        }

        // Aplica el multiplicador de daño después de la carga
        meleeDmg.dmg = meleeDmg.dmg * chargeMultiplier;

        meleeDmg.canDmg = true; // Habilita el daño

        // Inicializa el tween para rotar el brazo al ángulo final con una animación suave
        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0, 0, endRotation), duration)
            .SetEase(Ease.InOutCirc) // Configura el tipo de easing para la animación
            .OnComplete(() => SwingDone()); // Llama a SwingDone() cuando la animación termina
    }

    protected override void Update()
    {
        base.Update();

        if (rotationTween != null)
        {
            if (Time.timeScale == 0 && rotationTween.IsPlaying())
            {
                // Pausa la animación si Time.timeScale es 0 y la animación está en marcha
                rotationTween.Pause();
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animación si Time.timeScale es 1 y la animación está pausada
                rotationTween.Play();
            }
        }

    }

    void SwingDone()
    {
        // Restaura el daño del golpe a su valor original
        meleeDmg.dmg = meleeDmg.dmg / chargeMultiplier;

        meleeDmg.canDmg = false; // Desactiva el daño

        handMovement.enabled = true; // Habilita el movimiento de la mano

        // Restaura el color inicial de la espada
        spriteRenderer.material.color = initialColor;

        // Restaura el ángulo inicial del brazo
        armPosition.transform.DOLocalRotate(new Vector3(0, 0, armPosition.transform.localEulerAngles.z - swingAngle), 0);

        // Habilita la capacidad de golpear en MeleeHitController
        MeleeHitController.instance.candHit = true;
    }
}
