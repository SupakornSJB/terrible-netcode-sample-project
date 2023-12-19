using UnityEngine;
using Unity.Netcode;

public class EnemyStrikingDistanceCheck : NetworkBehaviour
{
    private GameObject PlayerTarget { get; set; }
    private EnemyBase _enemy;

    public void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!IsServer) return;
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetStrikingDistancBool(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!IsServer) return;
        if (collider.gameObject == PlayerTarget)
        {
            _enemy.SetStrikingDistancBool(false);
        }
    }
}
