using Unity.Netcode;
using UnityEngine;

namespace Attack
{
    public class Bullet : NetworkBehaviour
    {
        // Old Method 
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (!IsServer) return;
        //     GetComponent<NetworkObject>().Despawn();
        //     Destroy(gameObject);
        // }
        
        // New method with NetworkObjectPool
        private void OnCollisionEnter(Collision collision)
        {
            if (!IsServer || !collision.gameObject.CompareTag("Player")) return;
            var networkObj = GetComponent<NetworkObject>();
            networkObj.Despawn();

            // BUG (FATAL): Needs the prefab, not the game object (clone) to despawn
            // AKA: Can't just use gameObject in the ReturnNetworkObject Function
            NetworkObjectPool.Singleton.ReturnNetworkObject(networkObj, gameObject);
            // BUG ENDED
        }
    }
}
