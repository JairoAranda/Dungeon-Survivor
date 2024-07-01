using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(SOFinderPlayer))]
public class PlayerMovement : MonoBehaviour
{
    private SOPlayerInfo sOPlayerInfo;
    private float m_speed;
    private Rigidbody2D rb;
    private InputHandler inputHandler;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<InputHandler>();
        sOPlayerInfo = GetComponent<SOFinderPlayer>().sOPlayerInfo;

        m_speed = sOPlayerInfo.speed;
    }

    private void Update()
    {
        Vector2 movement = inputHandler.GetMovementInput();
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 movement)
    {
        rb.velocity = movement * m_speed;
    }
}
