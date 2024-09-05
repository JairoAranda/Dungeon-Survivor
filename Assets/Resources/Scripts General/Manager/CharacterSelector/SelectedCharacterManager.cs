using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharacterManager : MonoBehaviour
{
    public static SelectedCharacterManager instance;

    [HideInInspector]
    public GameObject player;

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


    public void SelectPlayer(GameObject character)
    {
        player = character;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        if (player == null)
        {
            Instantiate(testPlayer, Vector3.zero,Quaternion.identity);
        }
        else
        {
            Instantiate(player, Vector3.zero, Quaternion.identity);
        }
    }
}
