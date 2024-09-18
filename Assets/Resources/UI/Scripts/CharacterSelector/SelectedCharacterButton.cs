using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterButton : MonoBehaviour
{
    // Selecciona el personaje especificado.
    public void CharacterSelection(GameObject charac)
    {
        // Llama al método SelectPlayer del SelectedCharacterManager para seleccionar el personaje
        SelectedCharacterManager.instance.SelectPlayer(charac);
    }
}
