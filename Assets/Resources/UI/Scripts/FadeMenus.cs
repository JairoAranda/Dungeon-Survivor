using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeMenus : MonoBehaviour
{
    [SerializeField] float fadeDuration;

    private Image[] images;

    private TextMeshProUGUI[] texts;

    void Start()
    {
        images = GetComponentsInChildren<Image>();

        texts = GetComponentsInChildren<TextMeshProUGUI>();

        StartCoroutine(FadeTo(1));
    }

    IEnumerator FadeTo(float targetAlpha)
    {
        float[] currentAlphas = new float[images.Length];
        float[] alphaDifferences = new float[images.Length];

        float[] textCurrentAlphas = new float[texts.Length];
        float[] textAlphaDifferences = new float[texts.Length];

        // Almacena los valores alpha actuales de cada imagen
        for (int i = 0; i < images.Length; i++)
        {
            currentAlphas[i] = 1 - targetAlpha;
            alphaDifferences[i] = targetAlpha - currentAlphas[i];
        }

        // Almacena los valores alpha actuales de cada texto
        for (int i = 0; i < texts.Length; i++)
        {
            textCurrentAlphas[i] = 1 - targetAlpha;
            textAlphaDifferences[i] = targetAlpha - textCurrentAlphas[i];
        }

        float elapsedTime = 0f;

        // Realiza el fade durante el tiempo especificado
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            for (int i = 0; i < images.Length; i++)
            {
                // Calcula el nuevo alpha
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
