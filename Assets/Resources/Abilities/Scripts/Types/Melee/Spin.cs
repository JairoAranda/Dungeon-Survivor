using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spin : BaseMeleeAbility, IAbility
{
    [SerializeField] float duration = 1f;

    [SerializeField] int loops = 2;

    private Tween rotationTween;

    public void Ability()
    {
        Atributes();

        Rotate360();
    }

    void Rotate360()
    {
        float initialRotationZ = armPosition.transform.localEulerAngles.z;

        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0f, 0f, initialRotationZ + 360f), duration, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear) // Movimiento suave y continuo
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

    void EndAbility()
    {
        MeleeHitController.instance.candHit = true;

        meleeDmg.canDmg = false;

        handMovement.enabled = true;

    }
}
