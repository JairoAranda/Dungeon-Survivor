using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterButton : MonoBehaviour
{
    public void CharacterSelection(GameObject charac)
    {
        SelectedCharacterManager.instance.SelectPlayer(charac);
    }
}
