using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnManager : Singleton<EnemySpawnManager>
    {
        [SerializeField] private EnemyScriptableObject[] enemyTypes;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private NetworkVariable<bool> isSpawning = new NetworkVariable<bool>(false);
        [SerializeField] private GameObject improvedEnemyPrefab;

        public bool IsSpawning => isSpawning.Value;

        public void SetIsSpawn(bool value)
        {
            isSpawning.Value = value;
        }

        public void SetIsSpawn(Func<bool, bool> func)
        {
            var newValue = func(isSpawning.Value);
            isSpawning.Value = newValue;
        }

        public EnemyScriptableObject GetEnemyConfigById(int id)
        {
            if (enemyTypes.Length == 0 || id >= enemyTypes.Length || id < 0)
            {
                return null;
            }
            return enemyTypes[id];
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                return;
            }

            isSpawning.OnValueChanged += (_, current) =>
            {
                if (current)
                {
                    StartCoroutine(SpawnEnemy());
                }
                else
                {
                    StopCoroutine(SpawnEnemy());
                }
            };
        }

        public void TestSpawn()
        {
            var enemy = NetworkObjectPool.Singleton.GetNetworkObject(improvedEnemyPrefab, Vector3.zero, Quaternion.identity);
            enemy.Spawn();
        }

        private IEnumerator SpawnEnemy()
        {
            while (isSpawning.Value)
            {
                if (enemyTypes.Length == 0)
                {
                    yield return new WaitForSeconds(5.0f);
                    continue;
                }

                var randomPosition = new Vector3(UnityEngine.Random.Range(-5, 5), -3, UnityEngine.Random.Range(-5, 5));
                var enemyGameObject = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

                var enemyNetwork = enemyGameObject.GetComponent<NetworkObject>();
                enemyNetwork.Spawn();

                var enemy = enemyGameObject.GetComponent<global::Enemy.Enemy>();
                enemy.InitializeConfigId(UnityEngine.Random.Range(0, enemyTypes.Length));

                yield return new WaitForSeconds(5.0f);
            }
        }
    }
}
