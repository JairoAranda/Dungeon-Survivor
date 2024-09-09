using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSound : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SelectSound()
    {
        int sound = Random.Range(0, sounds.Length);

        audioSource.clip = sounds[sound];

        audioSource.Play();
    }
}
