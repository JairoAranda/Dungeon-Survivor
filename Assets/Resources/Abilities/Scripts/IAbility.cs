using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IAbility
{
    SOPlayerInfo sOPlayerInfo { get; set; }

    float cd { get; set; }

    float currentCD { get; set; }

    KeyCode keycode { get; set; }

    Sprite img { get; set; }

    Image CDimg { get; set; }

    GameObject go { get; set; }

    void Ability();
}
