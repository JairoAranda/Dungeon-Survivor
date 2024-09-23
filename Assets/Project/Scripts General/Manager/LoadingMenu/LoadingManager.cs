using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;

    [HideInInspector]
    public float progress; // Progreso de carga de la escena.

    [HideInInspector]
    public int sceneIndex; // �ndice de la escena que se va a cargar.

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
        SceneManager.sceneLoaded += StartLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartLoad;
    }

    // Inicia la carga de la escena cuando se ha cargado una nueva escena.
    void StartLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            progress = 0;

            StartCoroutine(LoadSceneAsync());
        }

    }

    // Carga una escena de manera as�ncrona y controla el progreso de carga.
    public IEnumerator LoadSceneAsync()
    {

        yield return new WaitForEndOfFrame();

        // Comienza a cargar la escena de forma as�ncrona, pero no cambia a�n
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Prevenir que la escena se active autom�ticamente cuando termine de cargar
        operation.allowSceneActivation = false;

        // Mientras la escena se carga, puedes actualizar una barra de progreso o mostrar algo
        while (!operation.isDone)
        {
            // El progreso es un valor entre 0 y 0.9, ya que 0.9 significa que ha cargado
            progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Si la escena ha terminado de cargar (lleg� al 90% del progreso), la activas
            if (operation.progress >= 0.9f)
            {
                // Aqu� podr�as agregar alguna condici�n para cambiar de escena, por ejemplo:
                // si el jugador pulsa un bot�n o se cumple cierto tiempo.
                // En este ejemplo, se activa inmediatamente:
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
