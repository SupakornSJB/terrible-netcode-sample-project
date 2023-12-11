using UnityEngine;
using System.Collections;
using Unity.Netcode;

namespace Enemy
{
    public class EnemyAttack : NetworkBehaviour
    {
        private global::Enemy.Enemy enemy;
        private GameObject player;
        private bool isReady = false;
        [SerializeField] private GameObject _bulletPrefab;

        private void Start()
        {
            enemy = GetComponent<global::Enemy.Enemy>();
            if (!enemy.isReady.Value)
            {
                enemy.isReady.OnValueChanged += (_, current) =>
                {
                    if (!current) return;
                    player = enemy.followingPlayer;
                    isReady = true;
                };
            }
            else
            {
                player = enemy.followingPlayer;
                isReady = true;
            }

            if (_bulletPrefab == null)
            {
                Debug.LogError("Bullet prefab is null");
                enabled = false;
            }
        }

        private void Initiate()
        {
            if (!IsServer) return;
            if (enemy.enemyConfig.isMelee)
            {
                StartCoroutine(StartShooting());
            }
            else 
            {
                StopCoroutine(StartShooting());
            }
        }

        private void Update()
        {
            transform.LookAt(player.transform);
        }

        public IEnumerator StartShooting()
        {
            while (true)
            {
                var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<NetworkObject>().Spawn();
                yield return new WaitForSeconds(5.0f);
            }
        }
    }
}
