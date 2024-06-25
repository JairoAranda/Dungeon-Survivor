using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Range(0.1f, 15)]
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private InputHandler inputHandler;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        Vector2 movement = inputHandler.GetMovementInput();
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 movement)
    {
        rb.velocity = movement * speed;
    }
}
