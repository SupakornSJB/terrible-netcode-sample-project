using UnityEngine;
using Unity.Netcode;

public class EnemyAggroedCheck : NetworkBehaviour
{
    private GameObject PlayerTarget { get; set; }
    private EnemyBase _enemy;

    public void Awake()
    {
        // TODO: Find a way to select players since there will be multiple players
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<EnemyBase>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!IsServer) return; 
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetAggroedState(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!IsServer) return;
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetAggroedState(false);
        }
    }
}
