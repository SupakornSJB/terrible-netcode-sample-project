using System;
using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(NetworkObject))]
    [Tooltip("This is a player health script")]
    public class PlayerHealth : NetworkBehaviour
    {
        [Tooltip("Player's health")] private readonly NetworkVariable<int> health = new NetworkVariable<int>();
        private TextMeshProUGUI healthText;

        private void Start()
        {
            healthText = GameObject.Find("Canvas/HealthDisplayText").GetComponent<TextMeshProUGUI>();
            healthText.text = "HP: " + health.Value;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Bullet") || !IsOwner || !IsClient) return;
            PlayerHitServerRpc();
            StartCoroutine(ChangeHealthTextColor());
        }

        private IEnumerator ChangeHealthTextColor()
        {
            healthText.color = Color.red;
            yield return new WaitForSeconds(5.0f);
            healthText.color = Color.white;
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                health.Value = 3;
            }

            if (IsOwner && IsClient)
            {
                health.OnValueChanged += (prev, current) => { healthText.text = "health " + current; };
            }
        }

        [ServerRpc]
        private void PlayerHitServerRpc()
        {
            health.Value -= 1;
        }
    }
}
