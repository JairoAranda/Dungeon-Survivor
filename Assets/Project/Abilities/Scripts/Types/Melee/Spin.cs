using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spin : BaseMeleeAbility, IAbility
{
    [Header("Spin Config")]
    [Range(0.1f , 3f)]
    [SerializeField] float duration = 1f; // Duración de una rotación completa
    [Range(2f, 10f)]
    [SerializeField] int loops = 2; // Número de veces que la rotación se repetirá

    private Tween rotationTween; // Tween para la rotación

    public void Ability()
    {
        // Configura los atributos necesarios para la habilidad
        Atributes();

        // Inicia la rotación
        Rotate360();
    }

    void Rotate360()
    {
        // Obtiene el ángulo Z actual de la rotación del brazo
        float initialRotationZ = armPosition.transform.localEulerAngles.z;

        // Configura el tween para rotar el brazo 360 grados
        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0f, 0f, initialRotationZ + 360f), duration, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear) // Establece un movimiento suave y continuo
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
        // Permite que el personaje pueda golpear nuevamente
        MeleeHitController.instance.candHit = true;

        // Desactiva el daño en el objeto de daño cuerpo a cuerpo
        meleeDmg.canDmg = false;

        // Habilita el movimiento de la mano
        handMovement.enabled = true;

        StartCD();
    }
}
