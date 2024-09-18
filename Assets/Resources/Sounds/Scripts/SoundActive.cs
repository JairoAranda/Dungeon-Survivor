using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RandomSound))]
public class SoundActive : MonoBehaviour
{
    RandomSound randomSound;

    protected virtual private void Start()
    {
        randomSound = GetComponent<RandomSound>();
    }

    // Reproduce un sonido usando el componente RandomSound cuando se activa el objeto.
    protected virtual void SoundEnable(GameObject go)
    {
        randomSound.SelectSound();
    }
}

