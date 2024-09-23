using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : BaseMeleeAbility, IAbility
{
    [Header("Throw Config")]
    [Range(1f, 15f)]
    [SerializeField] float moveDistance = 5f;  // Distancia que se mover� en X
    [Range(180f, 720f)]
    [SerializeField] float rotationSpeed = 360f;  // Velocidad de rotaci�n en grados por segundo
    [Range(0.1f, 15f)]
    [SerializeField] float duration = 2f;  // Duraci�n total de la animaci�n
    [Range(1f, 15f)]
    [SerializeField] int loops = 5; // N�mero de repeticiones de la rotaci�n

    private Tween rotationTween; // Tween para la rotaci�n
    private Tween moveTween; // Tween para el movimiento

    public void Ability()
    {
        // Configura los atributos necesarios para la habilidad
        Atributes();

        // Inicia la animaci�n de lanzar la espada
        ThrowWeapon();
    }

    void ThrowWeapon()
    {
        // Mueve la espada en el eje Y
        moveTween = meleeDmg.transform.DOLocalMoveY(meleeDmg.transform.localPosition.y + moveDistance, duration / 2)
            .SetEase(Ease.InOutSine)  // Configura la curva de animaci�n para suavizar el movimiento
            .SetLoops(2, LoopType.Yoyo); // Mueve hacia adelante y hacia atr�s dos veces


        // Rota la espada alrededor del eje Z a la velocidad especificada
        rotationTween = meleeDmg.transform.DOLocalRotate(new Vector3(0, 0, rotationSpeed), duration / loops, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)  // Establece una rotaci�n constante
            .SetLoops(loops, LoopType.Incremental) // Configura el n�mero de repeticiones de la rotaci�n
            .OnComplete(() => EndAbility()); // Llama a EndAbility() cuando la rotaci�n termina
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

                moveTween.Pause();
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animaci�n si Time.timeScale es 1 y la animaci�n est� pausada
                rotationTween.Play();

                moveTween.Play();
            }
        }

    }

    void EndAbility()
    {
        // Permite que el personaje pueda golpear nuevamente
        MeleeHitController.instance.candHit = true;

        // Desactiva el da�o en el objeto de da�o cuerpo a cuerpo
        meleeDmg.canDmg = false;

        // Habilita el movimiento de la mano
        handMovement.enabled = true;

        StartCD();

    }
}
