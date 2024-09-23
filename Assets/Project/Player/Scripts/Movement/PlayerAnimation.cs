using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputHandler))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform armPosition; // Referencia a la posición del brazo (usada para calcular el ángulo del jugador)

    private Animator animator; // Referencia al componente Animator para controlar las animaciones
    private InputHandler inputHandler; // Referencia al componente InputHandler para obtener la entrada de movimiento del jugador

    private void Start()
    {
        // Inicializa el Animator y el InputHandler al obtener los componentes necesarios del GameObject
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        // Calcula el ángulo entre la dirección derecha y la dirección actual del brazo
        float angle = Vector2.SignedAngle(Vector2.right, armPosition.right);

        // Obtiene la entrada de movimiento del jugador desde el InputHandler
        Vector2 movement = inputHandler.GetMovementInput();

        // Actualiza las animaciones según el movimiento y el ángulo
        UpdateAnimation(movement, angle);
    }

    private void UpdateAnimation(Vector2 movement, float angle)
    {
        // Si el jugador se está moviendo, activa la animación de correr
        if (movement != Vector2.zero)
        {
            animator.SetBool("isRuning", true);
        }
        else
        {
            // Si no hay movimiento, desactiva la animación de correr
            animator.SetBool("isRuning", false);
        }

        // Actualiza el valor del ángulo en el Animator
        animator.SetFloat("angle", angle);
    }
}
