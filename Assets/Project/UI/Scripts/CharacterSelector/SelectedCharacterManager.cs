using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharacterManager : MonoBehaviour
{
    public static SelectedCharacterManager instance; 

    [HideInInspector]
    public GameObject player; // El GameObject del personaje seleccionado.

    [Header("No need Main Menu")]
    [Tooltip("Esto es para que puedas probar sin necesidad de ir al Main Menu")]
    [SerializeField] GameObject testPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Asigna el GameObject del personaje seleccionado.
    public void SelectPlayer(GameObject character)
    {
        player = character;
    }

    // Se llama cuando una escena es cargada para hacer el spawn del jugador.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            SpawnPlayer();
        }
    }

    // Instancia al jugador en la posición (0, 0, 0) si no está ya instanciado.
    void SpawnPlayer()
    {
        if (player == null && PlayerStats.instance == null)
        {
            // Instancia el personaje de prueba si no hay personaje seleccionado y PlayerStats no existe
            Instantiate(testPlayer, Vector3.zero,Quaternion.identity);
        }
        else if (PlayerStats.instance == null)
        {
            // Instancia el personaje seleccionado si PlayerStats no existe
            Instantiate(player, Vector3.zero, Quaternion.identity);
        }
    }
}
