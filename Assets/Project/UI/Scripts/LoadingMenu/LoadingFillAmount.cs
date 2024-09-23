using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingFillAmount : MonoBehaviour
{
    [SerializeField] Image fillImg; // La imagen que se actualizará para mostrar el progreso de carga.

    private void Update()
    {
        // Actualiza el valor de fillAmount de la imagen basado en el progreso de carga del LoadingManager.
        fillImg.fillAmount = LoadingManager.instance.progress;
    }
}
