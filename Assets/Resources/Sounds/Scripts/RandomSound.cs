using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Array de clips de audio disponibles para reproducción.")]
    AudioClip[] sounds;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Selecciona un sonido aleatorio del array de sonidos y lo reproduce.
    public void SelectSound()
    {
        // Selecciona un índice aleatorio dentro del rango de la longitud del array de sonidos.
        int sound = Random.Range(0, sounds.Length);

        // Asigna el clip de audio seleccionado al AudioSource.
        audioSource.clip = sounds[sound];

        // Reproduce el clip de audio.
        audioSource.Play();
    }
}
