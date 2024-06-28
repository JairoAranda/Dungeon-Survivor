using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(InputHandler))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Transform handPosition;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private InputHandler inputHandler;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        float angle = Vector2.SignedAngle(Vector2.right, handPosition.right);
        Vector2 movement = inputHandler.GetMovementInput();
        UpdateAnimation(movement, angle);
    }

    private void UpdateAnimation(Vector2 movement, float angle)
    {
        if (movement != Vector2.zero)
        {
            UpdateWalkingAnimation(movement, angle);
        }
        else
        {
            UpdateIdleAnimation(angle);
        }
    }

    private void UpdateWalkingAnimation(Vector2 movement, float angle)
    {
        if (movement.x != 0)
        {
            if (angle <= 90 && angle >= -90)
            {
                SetAnimation("WalkRight", false);
            }
            else
            {
                SetAnimation("WalkLeft", true);
            }
        }
        else if (movement.y != 0)
        {
            spriteRenderer.flipX = false;

            if (angle < 180 && angle > 0)
            {
                animator.SetTrigger("WalkUp");
            }
            else
            {
                animator.SetTrigger("WalkDown");
            }
        }
    }

    private void UpdateIdleAnimation(float angle)
    {
        spriteRenderer.flipX = false;

        if (angle <= 45 && angle >= -45)
        {
            SetAnimation("Idle", false);
        }
        else if (angle <= -45 && angle >= -135)
        {
            SetAnimation("Idle", false);
        }
        else if (angle <= 135 && angle >= 45)
        {
            SetAnimation("Idle", false);
        }
        else
        {
            SetAnimation("Idle", false);
        }
    }

    private void SetAnimation(string triggerName, bool flipX)
    {
        animator.SetTrigger(triggerName);
        spriteRenderer.flipX = flipX;
    }
}
