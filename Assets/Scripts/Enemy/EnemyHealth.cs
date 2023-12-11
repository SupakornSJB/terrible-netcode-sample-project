using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(global::Enemy.Enemy))]
    public class EnemyHealth : NetworkBehaviour
    {
        private global::Enemy.Enemy enemy;
        [FormerlySerializedAs("HP_stat")] public NetworkVariable<int> hpStat = new NetworkVariable<int>(10);

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            enemy = GetComponent<global::Enemy.Enemy>();
            if (IsServer)
            {
                enemy.isReady.OnValueChanged += (_, current) =>
                {
                    if (current)
                    {
                        hpStat.Value = enemy.enemyConfig.hpStat;
                    }
                };
            }

            hpStat.OnValueChanged += (_, current) => 
            {
                if (IsServer && current <= 0)
                {
                    Destroy(gameObject);
                }
            };
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsServer && collision.gameObject.CompareTag("Bullet"))
            {
                hpStat.Value -= 60;
            }
        }
    }
}
