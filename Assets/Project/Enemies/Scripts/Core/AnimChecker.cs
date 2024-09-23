using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimChecker : MonoBehaviour
{
    private Animator animator; // Referencia al componente Animator del GameObject
    private void Start()
    {
        // Obtiene el componente Animator del GameObject al iniciar
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Compara la posición Y del jugador con la del objeto actual
        if (PlayerStats.instance.transform.position.y - 1f > transform.position.y)
        {
            // Si la posición Y del jugador es mayor (más alta) que la del objeto, ajusta el parámetro "isUp" a true
            animator.SetBool("isUp", true);
        }

        else
        {
            // De lo contrario, ajusta el parámetro "isUp" a false
            animator.SetBool("isUp", false);
        }
    }
}
