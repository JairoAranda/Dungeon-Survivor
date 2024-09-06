using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentButtonColor : MonoBehaviour
{
    [SerializeField] private Color color;

    [SerializeField] Image[] buttons;

    public void ChangeColorButton(Image currentButton)
    {
        foreach (var button in buttons)
        {
            button.color = Color.white;
        }

        currentButton.color = color;
    }
}
