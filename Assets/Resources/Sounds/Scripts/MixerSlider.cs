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

        // Cargar el volumen guardado desde PlayerPrefs o usar un valor por defecto (0.75)
        float savedVolume = PlayerPrefs.GetFloat(exposedParameter.ToString(), 0.75f);

        // Aplicar el volumen guardado al AudioMixer
        SetVolume(savedVolume);

        // Actualizar el slider con el valor guardado (entre 0 y 1)
        volumeSlider.value = savedVolume;

        // Añadir el listener para detectar cambios en el slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Método que ajusta el volumen del AudioMixer
    public void SetVolume(float value)
    {
        // Evitar problemas logarítmicos con valores muy pequeños
        if (value <= 0.0001f)
        {
            value = 0.0001f;  // Para evitar Log10(0), que es indefinido
        }

        // Convertir el valor del slider a una escala logarítmica (dB)
        float volume = Mathf.Log10(value) * 20;
        audioMixer.SetFloat(exposedParameter.ToString(), volume);

        // Guardar el valor del volumen en PlayerPrefs
        PlayerPrefs.SetFloat(exposedParameter.ToString(), value);
        PlayerPrefs.Save();  // Asegúrate de guardar los cambios
    }
}
