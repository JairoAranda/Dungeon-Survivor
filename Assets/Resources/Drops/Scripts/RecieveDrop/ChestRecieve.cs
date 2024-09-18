using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRecieve : GeneralReciveDrop
{
    protected override void AnimDone()
    {
        // Activa el men� de cofres del ChestManager
        ChestManager.instance.ActiveChestMenu();

        // Inicia la coroutine para desactivar el objeto del juego
        StartCoroutine(DestoyObject());
    }
}
