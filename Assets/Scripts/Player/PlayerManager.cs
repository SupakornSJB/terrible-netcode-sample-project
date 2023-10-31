using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        private readonly NetworkVariable<int> playersInGame = new NetworkVariable<int>();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            playersInGame.Value = 0;
        }

        public int PlayersInGame => playersInGame.Value;

        private void Start()
        {

            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                if (!NetworkManager.Singleton.IsServer) return;
                Debug.Log($"{id} just connected...");
                playersInGame.Value++;
            };

            NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
            {
                if (!NetworkManager.Singleton.IsServer) return;
                Debug.Log($"{id} just disconnected...");
                playersInGame.Value--;
            };
        }
    }
}
