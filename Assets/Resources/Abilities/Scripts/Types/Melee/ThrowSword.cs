using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : BaseMeleeAbility, IAbility
{
    [SerializeField] float moveDistance = 5f;  // Distancia que se mover� en X
    [SerializeField] float rotationSpeed = 360f;  // Grados de rotaci�n en Z
    [SerializeField] float duration = 2f;  // Duraci�n de la animaci�n
    [SerializeField] int loops = 5;

    private Tween rotationTween;
    private Tween moveTween;

    public void Ability()
    {
        Atributes();

        ThrowWeapon();
    }

    void ThrowWeapon()
    {
        // Mueve en el eje X durante la duraci�n especificada
        moveTween = meleeDmg.transform.DOLocalMoveY(meleeDmg.transform.localPosition.x + moveDistance, duration / 2)
            .SetEase(Ease.InOutSine)  // Puedes ajustar la curva de animaci�n con SetEase
            .SetLoops(2, LoopType.Yoyo);


        // Rota alrededor del eje Z a la velocidad especificada (vueltas continuas)
        rotationTween = meleeDmg.transform.DOLocalRotate(new Vector3(0, 0, rotationSpeed), duration / loops, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)  // Rotaci�n lineal para que sea constante
            .SetLoops(loops, LoopType.Incremental)
            .OnComplete(() => EndAbility());
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
        MeleeHitController.instance.candHit = true;

        meleeDmg.canDmg = false;

        handMovement.enabled = true;

    }
}
