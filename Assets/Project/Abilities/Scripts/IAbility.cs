using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public interface IAbility
{
    SOPlayerInfo sOPlayerInfo { get; set; }

    float cd { get; set; }

    float currentCD { get; set; }

    InputAction abilityAction { get; set; }

    Sprite img { get; set; }

    Image CDimg { get; set; }

    GameObject go { get; set; }

    bool isUsing { set; get; }

    void Ability();

    void StartCD();
}
