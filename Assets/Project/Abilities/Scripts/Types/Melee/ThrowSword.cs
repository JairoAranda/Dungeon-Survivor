using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : BaseMeleeAbility, IAbility
{
    [Header("Throw Config")]
    [Range(1f, 15f)]
    [SerializeField] float moveDistance = 5f;  // Distancia que se moverá en X
    [Range(180f, 720f)]
    [SerializeField] float rotationSpeed = 360f;  // Velocidad de rotación en grados por segundo
    [Range(0.1f, 15f)]
    [SerializeField] float duration = 2f;  // Duración total de la animación
    [Range(1f, 15f)]
    [SerializeField] int loops = 5; // Número de repeticiones de la rotación

    private Tween rotationTween; // Tween para la rotación
    private Tween moveTween; // Tween para el movimiento

    public void Ability()
    {
        // Configura los atributos necesarios para la habilidad
        Atributes();

        // Inicia la animación de lanzar la espada
        ThrowWeapon();
    }

    void ThrowWeapon()
    {
        // Mueve la espada en el eje Y
        moveTween = meleeDmg.transform.DOLocalMoveY(meleeDmg.transform.localPosition.y + moveDistance, duration / 2)
            .SetEase(Ease.InOutSine)  // Configura la curva de animación para suavizar el movimiento
            .SetLoops(2, LoopType.Yoyo); // Mueve hacia adelante y hacia atrás dos veces


        // Rota la espada alrededor del eje Z a la velocidad especificada
        rotationTween = meleeDmg.transform.DOLocalRotate(new Vector3(0, 0, rotationSpeed), duration / loops, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)  // Establece una rotación constante
            .SetLoops(loops, LoopType.Incremental) // Configura el número de repeticiones de la rotación
            .OnComplete(() => EndAbility()); // Llama a EndAbility() cuando la rotación termina
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

                moveTween.Pause();
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animación si Time.timeScale es 1 y la animación está pausada
                rotationTween.Play();

                moveTween.Play();
            }
        }

    }

    void EndAbility()
    {
        // Permite que el personaje pueda golpear nuevamente
        MeleeHitController.instance.candHit = true;

        // Desactiva el daño en el objeto de daño cuerpo a cuerpo
        meleeDmg.canDmg = false;

        // Habilita el movimiento de la mano
        handMovement.enabled = true;

        StartCD();

    }
}
