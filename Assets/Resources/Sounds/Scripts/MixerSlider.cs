using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerSlider : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;                  // Referencia al AudioMixer
    [SerializeField] AudioMixerParameters exposedParameter;  // El nombre del parámetro expuesto
    private Slider volumeSlider;                    // Referencia al Slider


    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Inicializar el slider con el valor actual del AudioMixer
        float currentVolume;
        audioMixer.GetFloat(exposedParameter.ToString(), out currentVolume);
        volumeSlider.value = Mathf.InverseLerp(-80f, 0f, currentVolume);

        // Añadir el listener para detectar cambios en el slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Método que ajusta el volumen del AudioMixer
    public void SetVolume(float value)
    {
        // Convertir el valor del slider a una escala logarítmica (dB)
        float volume = Mathf.Lerp(-80f, 0f, value);
        audioMixer.SetFloat(exposedParameter.ToString(), volume);
    }
}
