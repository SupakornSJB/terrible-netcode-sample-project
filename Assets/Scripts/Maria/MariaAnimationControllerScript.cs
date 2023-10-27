using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MariaAnimationControllerScript : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("IsWalking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("IsSprinting", true);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("IsSprinting", false);
        }
    }
}
