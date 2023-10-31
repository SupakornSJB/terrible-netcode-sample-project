using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Animator))]
public class MariaAnimationControllerScript : NetworkBehaviour
{
    private Animator animator;
    private PlayerInput.OnFootActions onFoot;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsSprinting = Animator.StringToHash("IsSprinting");

    public void Start()
    {
        animator = GetComponent<Animator>();
        onFoot = GetComponentInParent<InputManager>().OnFoot;
        Debug.Log("Server owned: " + IsOwnedByServer);

        if (!IsOwner || !IsClient) return;
        onFoot.Sprint.performed += (ctx) => SetSprintingServerRpc(true);
        onFoot.Sprint.canceled += (ctx) => SetSprintingServerRpc(false);
    }

    public void Update()
    {
        if (!IsOwner || !IsClient) return;
        SetWalkingServerRpc(onFoot.Movement.ReadValue<Vector2>().sqrMagnitude > 0.1f);
    }

    [ServerRpc]
    private void SetWalkingServerRpc(bool value)
    {
        animator.SetBool(IsWalking, value);
    }

    [ServerRpc]
    private void SetSprintingServerRpc(bool value)
    {
        animator.SetBool(IsSprinting, value);
    }
}
