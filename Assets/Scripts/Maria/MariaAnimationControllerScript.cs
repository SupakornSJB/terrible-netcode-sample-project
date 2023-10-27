using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Animator))]
public class MariaAnimationControllerScript : NetworkBehaviour
{
    Animator animator;
    PlayerInput.OnFootActions onFoot;

    void Start()
    {
        animator = GetComponent<Animator>();
        onFoot = GetComponentInParent<InputManager>().onFoot;
        Debug.Log("Server owned: " + IsOwnedByServer);

        if (IsOwner && IsClient)
        {
            /* onFoot.Sprint.performed += (ctx) => animator.SetBool("IsSprinting", true); */
            /* onFoot.Sprint.canceled += (ctx) => animator.SetBool("IsSprinting", false); */
            onFoot.Sprint.performed += (ctx) => SetSprintingServerRpc(true);
            onFoot.Sprint.canceled += (ctx) => SetSprintingServerRpc(false);
        }
    }

    void Update()
    {
        if (IsOwner && IsClient)
        {
            if (onFoot.Movement.ReadValue<Vector2>().sqrMagnitude > 0.1f)
            {
                /* animator.SetBool("IsWalking", true); */
                SetWalkingServerRpc(true);
            }
            else
            {
                /* animator.SetBool("IsWalking", false); */
                SetWalkingServerRpc(false);
            }
        }
    }

    [ServerRpc]
    void SetWalkingServerRpc(bool value)
    {
        animator.SetBool("IsWalking", value);
    }

    [ServerRpc]
    void SetSprintingServerRpc(bool value)
    {
        animator.SetBool("IsSprinting", value);
    }
}
