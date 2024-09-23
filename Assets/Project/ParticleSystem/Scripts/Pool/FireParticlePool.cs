using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticlePool : GeneralPool
{
    public static FireParticlePool instance;

    int fireNumber = -1; // Índice para rastrear el siguiente objeto de partículas de fuego a usar

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
    }


    public IEnumerator FireFollow(GameObject target, float time)
    {
        fireNumber++;

        // Si el índice excede el tamaño del pool, reinícialo a 0 para reutilizar objetos
        if (fireNumber > poolSize - 1)
        {
            fireNumber = 0;
        }

        // Obtiene el siguiente objeto de partículas de fuego del pool
        GameObject fire = typesInstances[fireNumber];

        float elapsedTime = 0;

        // Mientras el tiempo transcurrido sea menor que el tiempo especificado
        while (elapsedTime < time)
        {
            // Establece la posición del objeto de partículas de fuego en la posición del objetivo
            fire.transform.position = target.transform.position;
            fire.SetActive(true); // Activa el objeto de partículas de fuego
            elapsedTime += Time.deltaTime; // Incrementa el tiempo transcurrido
            yield return null; 
        }

        fire.SetActive(false); // Desactiva el objeto de partículas de fuego después de que el tiempo ha pasado
    }
}
