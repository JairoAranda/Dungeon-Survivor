using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] SOPlayerInfo sOPlayerInfo; // Referencia al ScriptableObject que contiene la informaci�n del jugador
    [SerializeField] TextMeshProUGUI[] stats; // Array de componentes TextMeshProUGUI para mostrar las estad�sticas

    void Start()
    {
        // Obtiene todos los campos del ScriptableObject SOPlayerInfo
        FieldInfo[] fields = sOPlayerInfo.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        int i = 0;

        // Recorre cada campo para actualizar las estad�sticas
        foreach (FieldInfo field in fields)
        {
            // Verifica si el campo es de tipo float
            if (field.FieldType == typeof(float))
            {
                if (i < stats.Length) // Aseg�rate de no exceder el tama�o del array de stats
                {
                    // Capitaliza el nombre del campo para mostrarlo
                    string name = StringUtils.CapitalizeFirstLetter(field.Name);
                    // Obtiene el valor del campo
                    float value = (float)field.GetValue(sOPlayerInfo) * 10;

                    // Si el valor es distinto de cero, actualiza el texto del componente TextMeshProUGUI
                    if (value != 0f)
                    {
                        if (value % 1 == 0)
                        {
                            stats[i].text = "- " + name + " : " + value.ToString("F0");
                        }
                        else
                        {
                            stats[i].text = "- " + name + " : " + value.ToString("F1");
                        }
                        
                    }

                    else
                    {
                        // Si el valor es cero, destruye el objeto de texto
                        Destroy(stats[i].gameObject);
                    }
                    
                    i++;
                }
            }
        }
    }
}
