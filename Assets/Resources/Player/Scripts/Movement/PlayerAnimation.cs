using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputHandler))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform armPosition;

    private Animator animator;
    private InputHandler inputHandler;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        float angle = Vector2.SignedAngle(Vector2.right, armPosition.right);
        Vector2 movement = inputHandler.GetMovementInput();
        UpdateAnimation(movement, angle);
    }

    private void UpdateAnimation(Vector2 movement, float angle)
    {
        if (movement != Vector2.zero)
        {
            animator.SetBool("isRuning", true);
        }
        else
        {
            animator.SetBool("isRuning", false);
        }

        animator.SetFloat("angle", angle);
    }

}
