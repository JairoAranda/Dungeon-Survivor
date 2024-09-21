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
    [SerializeField] float duration = 1f; // Duraci�n de una rotaci�n completa
    [Range(2f, 10f)]
    [SerializeField] int loops = 2; // N�mero de veces que la rotaci�n se repetir�

    private Tween rotationTween; // Tween para la rotaci�n

    public void Ability()
    {
        // Configura los atributos necesarios para la habilidad
        Atributes();

        // Inicia la rotaci�n
        Rotate360();
    }

    void Rotate360()
    {
        // Obtiene el �ngulo Z actual de la rotaci�n del brazo
        float initialRotationZ = armPosition.transform.localEulerAngles.z;

        // Configura el tween para rotar el brazo 360 grados
        rotationTween = armPosition.transform.DOLocalRotate(new Vector3(0f, 0f, initialRotationZ + 360f), duration, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear) // Establece un movimiento suave y continuo
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
            }

            else if (Time.timeScale == 1 && !rotationTween.IsPlaying())
            {
                // Reanuda la animaci�n si Time.timeScale es 1 y la animaci�n est� pausada
                rotationTween.Play();
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
