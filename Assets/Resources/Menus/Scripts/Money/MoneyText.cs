using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;

    private void FixedUpdate()
    {
        money.text = MoneyManager.instance.money + " $";
    }
}
