using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using TMPro;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] SOPlayerInfo sOPlayerInfo;
    [SerializeField] TextMeshProUGUI[] stats;

    void Start()
    {
        FieldInfo[] fields = sOPlayerInfo.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        int i = 0;

        foreach (FieldInfo field in fields)
        {
            // Verifica si el campo es de tipo float
            if (field.FieldType == typeof(float))
            {
                if (i < stats.Length) // Aseg�rate de no exceder el tama�o del array de stats
                {
                    string name = StringUtils.CapitalizeFirstLetter(field.Name);
                    float value = (float)field.GetValue(sOPlayerInfo);

                    if (value != 0f)
                    {
                        stats[i].text = "- " + name + " : " + value.ToString();
                    }

                    else
                    {
                        Destroy(stats[i].gameObject);
                    }
                    
                    i++;
                }
            }
        }
    }


}
