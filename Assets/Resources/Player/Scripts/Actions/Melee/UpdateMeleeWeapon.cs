using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMeleeWeapon : MonoBehaviour
{
    IEffectType _effect; // Interfaz para el tipo de efecto del arma cuerpo a cuerpo

    MeleeDmg _meleeDmg; // Componente para gestionar el daño cuerpo a cuerpo

    private void Start()
    {
        // Obtiene el componente IEffectType asociado al arma
        _effect = GetComponent<IEffectType>();

        // Asigna el efecto al controlador de golpes cuerpo a cuerpo
        MeleeHitController.instance.effect = _effect;

        // Obtiene el componente MeleeDmg asociado al arma
        _meleeDmg = GetComponent<MeleeDmg>();

        // Asigna el componente MeleeDmg al controlador de golpes cuerpo a cuerpo
        MeleeHitController.instance.meleeDmg = _meleeDmg;
    }
}
