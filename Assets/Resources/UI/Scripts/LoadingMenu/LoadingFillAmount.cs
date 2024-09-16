using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingFillAmount : MonoBehaviour
{
    [SerializeField] Image fillImg;

    private void Update()
    {
        fillImg.fillAmount = LoadingManager.instance.progress;
    }
}
