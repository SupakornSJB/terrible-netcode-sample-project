using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MariaAnimationControllerScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("IsWalking");
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
