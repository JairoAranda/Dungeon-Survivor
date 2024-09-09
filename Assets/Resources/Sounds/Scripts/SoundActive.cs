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

    protected virtual void SoundEnable(GameObject go)
    {
        randomSound.SelectSound();
    }
}

