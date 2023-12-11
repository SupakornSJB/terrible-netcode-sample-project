using Unity.Netcode;

namespace Attack
{
    public class Bullet : NetworkBehaviour 
    {
        private void OnCollisionEnter()
        {
            if (!IsServer) return;
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }
    }
}
