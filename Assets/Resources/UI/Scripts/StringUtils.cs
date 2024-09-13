using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtils
{
    public static string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        // Capitaliza la primera letra
        str = char.ToUpper(str[0]) + str.Substring(1);

        // Añade un espacio antes de cada letra mayúscula (excepto la primera)
        for (int i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
            {
                str = str.Insert(i, " ");
                i++; // Ajustar el índice porque se ha insertado un espacio
            }
        }

        return str;
    }
}
