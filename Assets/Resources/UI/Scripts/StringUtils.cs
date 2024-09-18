using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{
    // Capitaliza la primera letra de una cadena y a�ade un espacio antes de cada letra may�scula (excepto la primera)
    public static string CapitalizeFirstLetter(string str)
    {
        // Verifica si la cadena est� vac�a o es nula
        if (string.IsNullOrEmpty(str)) return str;

        // Capitaliza la primera letra
        str = char.ToUpper(str[0]) + str.Substring(1);

        // A�ade un espacio antes de cada letra may�scula (excepto la primera)
        for (int i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                // Inserta un espacio antes de la letra may�scula
                str = str.Insert(i, " ");
                i++; // Ajustar el �ndice porque se ha insertado un espacio
            }
        }

        return str;
    }
}
