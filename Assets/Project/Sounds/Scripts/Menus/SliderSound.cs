using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;    // Referencia al AudioSource que reproduce el sonido del slider
    [SerializeField] float volumeThreshold = 0.1f; // Umbral de cambio para reproducir el sonido
    [SerializeField] float soundCooldown = 0.5f;  // Tiempo m�nimo entre sonidos
    private Slider volumeSlider;        // Referencia al Slider
    private float lastVolume;          // Almacena el �ltimo valor del slider
    private float lastSoundTime;       // Almacena el tiempo del �ltimo sonido reproducido

    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Inicializamos el valor del �ltimo volumen
        lastVolume = volumeSlider.value;

        // A�adir el listener para detectar cambios en el slider
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    // M�todo que se llama cuando el valor del slider cambia
    public void OnSliderValueChanged(float value)
    {
        // Comprobar si el cambio es mayor que el umbral
        if (Mathf.Abs(value - lastVolume) > volumeThreshold && Time.time - lastSoundTime >= soundCooldown)
        {
            audioSource.Play();

            lastSoundTime = Time.time;
        }

        lastVolume = value;
    }
}
