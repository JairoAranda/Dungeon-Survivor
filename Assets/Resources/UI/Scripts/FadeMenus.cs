using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeMenus : MonoBehaviour
{
    [SerializeField] float fadeDuration; // Duración del efecto de desvanecimiento

    private Image[] images; // Array para almacenar imágenes en el menú
    private TextMeshProUGUI[] texts; // Array para almacenar textos en el menú

    void Start()
    {
        // Obtén todas las imágenes y textos hijos del objeto actual
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();

        // Inicia el proceso de desvanecimiento a un alpha de 1 (completamente visible)
        StartCoroutine(FadeTo(1));
    }

    // Realiza un desvanecimiento a un alpha objetivo durante un tiempo especificado.
    IEnumerator FadeTo(float targetAlpha)
    {
        float[] currentAlphas = new float[images.Length];
        float[] alphaDifferences = new float[images.Length];

        float[] textCurrentAlphas = new float[texts.Length];
        float[] textAlphaDifferences = new float[texts.Length];

        // Almacena los valores alpha actuales de cada imagen
        for (int i = 0; i < images.Length; i++)
        {
            currentAlphas[i] = 1 - targetAlpha; // Obtener alpha actual
            alphaDifferences[i] = targetAlpha - currentAlphas[i]; // Diferencia entre el alpha objetivo y el actual
        }

        // Almacena los valores alpha actuales de cada texto
        for (int i = 0; i < texts.Length; i++)
        {
            textCurrentAlphas[i] = 1 - targetAlpha; // Obtener alpha actual
            textAlphaDifferences[i] = targetAlpha - textCurrentAlphas[i]; // Diferencia entre el alpha objetivo y el actual
        }

        float elapsedTime = 0f; // Tiempo transcurrido desde el inicio del desvanecimiento

        // Realiza el fade durante el tiempo especificado
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime; 

            for (int i = 0; i < images.Length; i++)
            {
                // Calcula el nuevo alpha basado en el tiempo transcurrido
                float newAlpha = Mathf.Clamp01(currentAlphas[i] + (alphaDifferences[i] * (elapsedTime / fadeDuration)));

                // Aplica el nuevo alpha a la imagen
                Color color = images[i].color;
                color.a = newAlpha;
                images[i].color = color;
            }

            // Aplicar fade a todos los textos
            for (int i = 0; i < texts.Length; i++)
            {
                float newAlpha = Mathf.Clamp01(textCurrentAlphas[i] + (textAlphaDifferences[i] * (elapsedTime / fadeDuration)));
                Color color = texts[i].color;
                color.a = newAlpha;
                texts[i].color = color;
            }

            yield return null;
        }

        // Asegúrate de que todas las imágenes y textos lleguen al alpha objetivo
        for (int i = 0; i < images.Length; i++)
        {
            Color finalColor = images[i].color;
            finalColor.a = targetAlpha;
            images[i].color = finalColor;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            Color finalColor = texts[i].color;
            finalColor.a = targetAlpha;
            texts[i].color = finalColor;
        }
    }

}
