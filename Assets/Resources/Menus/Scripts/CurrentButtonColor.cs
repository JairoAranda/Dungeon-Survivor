using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentButtonColor : MonoBehaviour
{
    [SerializeField] private Color color;

    [SerializeField] Image button;

    private void OnEnable()
    {
        button.color = color;
    }

    private void OnDisable()
    {
        button.color = Color.white;
    }
}
