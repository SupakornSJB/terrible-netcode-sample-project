using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerHUD : NetworkBehaviour
{
    private readonly NetworkVariable<NetworkString> playersName = new NetworkVariable<NetworkString>();
    private TextMeshProUGUI localPlayerOverlay; 
    private bool overlaySet = false;

    public void Start()
    {
        localPlayerOverlay = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playersName.Value = $"Player {OwnerClientId}";
        }
    }

    private void SetOverlay()
    {
        localPlayerOverlay.text = playersName.Value;
    }

    public void Update()
    {
        if (!overlaySet && !string.IsNullOrEmpty(playersName.Value)) 
        {
            SetOverlay();
            overlaySet = true;
        }
    }

}
