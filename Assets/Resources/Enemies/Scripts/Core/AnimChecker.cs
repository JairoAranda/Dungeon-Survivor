using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimChecker : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (PlayerStats.instance.transform.position.y - 1f > transform.position.y)
        {
            animator.SetBool("isUp", true);
        }

        else
        {
            animator.SetBool("isUp", false);
        }
    }
}